using Library.Data;
using Library.DTO;
using Library.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/borrow-request")]
    public class BorrowRequestController : Controller
    {
        private LibraryContext _libraryContext;
        public BorrowRequestController(LibraryContext context)
        {
            _libraryContext = context;
        }

        [HttpGet("pending-requests")]
        public async Task<ActionResult<IEnumerable<BorrowRequestResponseDTO>>> GetPendingRequests()
        {
            var requests = await _libraryContext.BorrowRequest
            .Where(r => r.Approved == null)
            .Include(r => r.BorrowsBook)
                .ThenInclude(bb => bb.User)
            .Include(r => r.BorrowsBook)
                .ThenInclude(bb => bb.BookCopy)
                .ThenInclude(bc => bc.Book)
            .Select(r => new BorrowRequestResponseDTO
            {
                RequestId = r.BorrowExtensionRequestId,
                BorrowsBookId = r.BorrowsBookId,
                UserEmail = r.BorrowsBook.User.Email,
                BookTitle = r.BorrowsBook.BookCopy.Book.Title,
                BookCopyId = r.BorrowsBook.BookCopyId,
                CurrentReturnDate = r.BorrowsBook.ReturnDate,
                RequestDate = r.RequestedAt,
                Approved = r.Approved,
            })
            .ToListAsync();

            return Ok(requests);
        }

        [HttpPost("process-extension")]
        public async Task<ActionResult> ProcessExtensionRequest([FromBody] BorrowRequestDTO dto)
        {
            var request = await _libraryContext.BorrowRequest
                .Include(r => r.BorrowsBook)
                .FirstOrDefaultAsync(r => r.BorrowExtensionRequestId == dto.RequestId);

            if (request == null) return NotFound("Запиту не знайдено.");
            if (request.Approved != null) return BadRequest("Запит уже оброблено.");

            if (dto.Decision.ToLower() == "approve")
            {
                request.BorrowsBook.ReturnDate = request.BorrowsBook.ReturnDate.AddDays(7);
                request.Approved = true;
                await _libraryContext.SaveChangesAsync();
                return Ok("Термін продовжено.");
            }
            else if (dto.Decision.ToLower() == "reject")
            {
                request.Approved = false;
                await _libraryContext.SaveChangesAsync();
                return Ok("Відмовлено у продовжені терміну.");
            }

            return BadRequest("Помилка");
        }

        [Authorize]
        [HttpPost("request-extension/{borrowId}")]
        public async Task<ActionResult> RequestExtension(int borrowId)
        {
            var borrow = await _libraryContext.BorrowsBook.FindAsync(borrowId);
            if (borrow == null)
                return NotFound("Не знайдено запису про позичання");

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            if (borrow.IsReturned)
                return BadRequest("Ця книга вже повернута, її неможливо повернути.");

            if (borrow.ReturnDate < today || borrow.ReturnDate > today.AddDays(2))
                return BadRequest("Ви можете повернути книгу тільки в останніх два дні.");

            var existingRequest = await _libraryContext.BorrowRequest
                .FirstOrDefaultAsync(r => r.BorrowsBookId == borrowId && r.Approved == null);

            if (existingRequest != null)
                return BadRequest("Ваш запит вже обробляється!");

            var request = new BorrowExtensionRequest
            {
                BorrowsBookId = borrowId,
                Approved = null,
                RequestedAt = DateTime.UtcNow
            };

            await _libraryContext.BorrowRequest.AddAsync(request);
            await _libraryContext.SaveChangesAsync();

            return Ok(new { message = "Ваш запит надіслано." });
        }


        [HttpGet("has-pending-request/{borrowId}")]
        public async Task<ActionResult<bool>> HasPendingRequest(int borrowId)
        {
            var exists = await _libraryContext.BorrowRequest
                .AnyAsync(r => r.BorrowsBookId == borrowId && r.Approved == null);

            return Ok(exists);
        }
    }
}
