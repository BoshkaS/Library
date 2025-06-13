using System.Net;
using Library.Data;
using Library.DTO;
using Library.DTO.Library_kursova.DTO;
using Library.Entities;
using Library.Interfaces;
using Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowsBookController : Controller
    {
        private LibraryContext libraryContext;
        private readonly IUserRatingService ratingService;
        private IUserContextService userContextService;

        public BorrowsBookController(LibraryContext context, IUserRatingService ratingService, IUserContextService userContextService)
        {
            this.libraryContext = context;
            this.ratingService = ratingService;
            this.userContextService = userContextService;
        }

        [HttpGet("all-borrows/{userId}")]
        public async Task<ActionResult<IEnumerable<BorrowedBookResponseDTO>>> GetBorrowsBooks(int userId)
        {
            //var userId = this.userContextService.GetCurrentUserId();

            var borrowedBooks = await libraryContext.BorrowsBook
                .Where(bb => bb.UserId == userId)
                .Include(bb => bb.BookCopy)
                .ThenInclude(bc => bc.Book)
                .Select(bb => new BorrowedBookResponseDTO
                {
                    BorrowsBookId = bb.BorrowsBookId,
                    BookId = bb.BookCopy.BookId,
                    Title = bb.BookCopy.Book.Title,
                    BookImage = bb.BookCopy.Book.BookImage,
                    BorrowDate = bb.BorrowDate,
                    ReturnDate = bb.ReturnDate,
                    IsReturned = bb.IsReturned,
                    ActualReturnDate = bb.ActualReturnDate,
                })
                .ToListAsync();

            borrowedBooks = borrowedBooks
                .OrderBy(bb => bb.IsReturned)
                .ThenBy(bb => bb.IsReturned ? -bb.ReturnDate.DayNumber : bb.ReturnDate.DayNumber)
                .ToList();

            return Ok(borrowedBooks);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("addborrows")]
        public async Task<ActionResult> AddBorrowsBook([FromBody]BorrowsBookDTO borrowsBookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var availableCopy = await libraryContext.BookCopy
                    .Where(bc => bc.BookId == borrowsBookDTO.BookId && !libraryContext.BorrowsBook
                    .Any(bb =>
                        bb.BookCopyId == bc.BookCopyId &&
                        bb.ReturnDate > DateOnly.FromDateTime(DateTime.UtcNow) &&
                        !bb.IsReturned))
                    .FirstOrDefaultAsync();

            if (availableCopy == null)
            {
                return BadRequest("Не має вільних копій книг.");
            }

            var borrow = new BorrowsBook
            {
                UserId = borrowsBookDTO.UserId,
                BookCopyId = availableCopy.BookCopyId,
                BorrowDate = DateOnly.FromDateTime(DateTime.UtcNow),
                ReturnDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(14))
            };

            await libraryContext.BorrowsBook.AddAsync(borrow);
            await libraryContext.SaveChangesAsync();

            return Ok(new BorrowedBookMessageDTO
            {
                Message = "Книгу позичено.",
                BorrowId = borrow.BorrowsBookId,
                BookCopyId = availableCopy.BookCopyId
            });
        }

        [Authorize(Roles = "admin")]
        [HttpPost("addborrowfromreservation")]
        public async Task<ActionResult> AddBorrowsBookFromReservation([FromBody] BorrowFromReservationDTO borrowsBookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var borrow = new BorrowsBook
            {
                UserId = borrowsBookDTO.UserId,
                BookCopyId = borrowsBookDTO.BookCopyId,
                BorrowDate = DateOnly.FromDateTime(DateTime.UtcNow),
                ReturnDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(14))
            };

            await libraryContext.BorrowsBook.AddAsync(borrow);
            await libraryContext.SaveChangesAsync();

            return Ok(new BorrowedBookMessageDTO
            {
                Message = "Книгу позичено.",
                BorrowId = borrow.BorrowsBookId,
                BookCopyId = borrowsBookDTO.BookCopyId
            });
        }

        [Authorize(Roles = "admin")]
        [HttpPost("return-book/{borrowId}")]
        public async Task<ActionResult> ReturnBook(int borrowId, [FromQuery] BookCondition condition)
        {
            var borrow = await libraryContext.BorrowsBook.FindAsync(borrowId);
            if (borrow == null)
                return NotFound("Позичення не знайдено");

            if (borrow.IsReturned)
                return BadRequest("Цю книгу вже повернули.");

            borrow.IsReturned = true;
            borrow.ActualReturnDate = DateOnly.FromDateTime(DateTime.UtcNow);

            await libraryContext.SaveChangesAsync();

            int daysLate = (borrow.ActualReturnDate.Value.DayNumber - borrow.ReturnDate.DayNumber);

            if (daysLate > 0)
            {
                await ratingService.PenalizeLateReturn(borrow.UserId, daysLate);
            }
            else
            {
                await ratingService.RewardForGoodReturn(borrow.UserId);
            }

            if (condition == BookCondition.Damaged)
            {
                await ratingService.PenalizeForDamagedBook(borrow.UserId);
            }

            return Ok(new { message = "Книгу повернуто." });
        }
    }
}

