using AutoMapper.Execution;
using Library.Data;
using Library.DTO;
using Library.Entities;
using Library.Interfaces;
using Library.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private LibraryContext _libraryContext;
        private IUserRatingService userRatingService;
        private IUserContextService userContextService;
        public ReservationController(LibraryContext context, IUserRatingService userRatingService, IUserContextService userContextService)
        {
            this._libraryContext = context;
            this.userRatingService = userRatingService;
            this.userContextService = userContextService;
        }

        [HttpGet("all-reservations")]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations()
        {
            await ProcessExpiredReservationsAsync(); // handle expired ones first

            var userId = this.userContextService.GetCurrentUserId();

            var bookmarkBooks = await _libraryContext.Reservation
                .Where(x => x.UserId == userId && x.IsActive)
                .Include(x => x.BookCopy)
                .Select(x => new ReservationDTO
                {
                    ReservationId = x.ReservationId,
                    BookCopyId = x.BookCopyId,
                    BookTitle = x.BookCopy.Book.Title,
                    BookImage = x.BookCopy.Book.BookImage,
                    ReservedAt = x.ReservedAt,
                    ExpiresAt = x.ExpiresAt,
                    BookId = x.BookCopy.BookId,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            return Ok(bookmarkBooks);
        }

        private async Task ProcessExpiredReservationsAsync()
        {
            var now = DateTime.UtcNow;

            var expiredReservations = await _libraryContext.Reservation
                .Where(r => r.IsActive)
                .ToListAsync();

            var trulyExpired = expiredReservations
                .Where(r => r.ExpiresAt < now)
                .ToList();

            foreach (var reservation in trulyExpired)
            {
                reservation.IsActive = false;
                await userRatingService.PenalizeExpiredReservation(reservation.UserId);
            }

            await _libraryContext.SaveChangesAsync();
        }

        [HttpPut("cancel-reservation/{reservationId}")]
        public async Task<ActionResult> CancelReservation(int reservationId)
        {
            var reservation = await _libraryContext.Reservation
                .FirstOrDefaultAsync(r => r.ReservationId == reservationId);

            if (reservation == null)
                return NotFound("Резервацію не знайдено.");

            if (!reservation.IsActive)
                return BadRequest("Резервація вже скасована.");

            reservation.IsActive = false;
            await _libraryContext.SaveChangesAsync();

            await userRatingService.PenalizeLateReservationCancellation(reservation.UserId, reservation.ReservedAt);

            return Ok(new { message = "Резервацію успішно скасовано." });
        }

        [HttpPost("add-reservation")]
        public async Task<ActionResult> AddReservation([FromBody] CreateReservationDTO dto)
        {
            var userId = this.userContextService.GetCurrentUserId();
            var user = await GetUserById(userId);
            if (user == null)
                return NotFound("UsКористувача не знайдено.");

            if (!user.IsMember)
                return NotFound("Ваш акаунт має підтвердити бібліотекар.");

            if (user.Rating <= 5)
                return NotFound("Рейтинг користувача має бути більше 5.");

            var activeBorrows = await _libraryContext.BorrowsBook
                .CountAsync(b => b.UserId == user.Id && !b.IsReturned);

            if (activeBorrows >= 4)
                return BadRequest("У вас вже є 4 позичання");

            var activeReservations = await _libraryContext.Reservation
                .CountAsync(r => r.UserId == user.Id && r.IsActive);

            if (activeReservations >= 2)
                return BadRequest("Досягнуто максимальної кількості резервації.");

            var availableCopy = await _libraryContext.BookCopy
                .Where(bc => bc.BookId == dto.BookId)
                .Where(bc =>
                    !_libraryContext.Reservation.Any(r =>
                        r.BookCopyId == bc.BookCopyId &&
                        r.ReservedAt.AddDays(1) > DateTime.UtcNow) &&
                    !_libraryContext.BorrowsBook.Any(bb =>
                        bb.BookCopyId == bc.BookCopyId &&
                        !bb.IsReturned &&
                        bb.ReturnDate > DateOnly.FromDateTime(DateTime.UtcNow)))
                .FirstOrDefaultAsync();


            if (availableCopy == null)
                return NotFound("Немає доступної копії книги.");

            var reservation = new Reservation
            {
                BookCopyId = availableCopy.BookCopyId,
                UserId = user.Id,
                ReservedAt = DateTime.UtcNow,
                IsActive = true,
            };

            _libraryContext.Reservation.Add(reservation);
            await _libraryContext.SaveChangesAsync();

            return Ok(new { message = "Reservation successful." });
        }


        public async Task<User> GetUserById(int id)
        {
            var user = await _libraryContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }
    }
}
