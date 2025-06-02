using System.Security.Claims;
using API.Extensions;
using Library.Data;
using Library.DTO;
using Library.Entities;
using Library.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookmarkBookController : Controller
    {
        private LibraryContext _libraryContext;
        private IUserContextService userContextService;
        public BookmarkBookController(LibraryContext context, IUserContextService userContextService)
        {
            this._libraryContext = context;
            this.userContextService = userContextService;
        }

        [HttpGet("bookmark")]
        public async Task<ActionResult<bool>> GetBookmarkBook(int bookId)
        {
            var userId = this.userContextService.GetCurrentUserId();
            var bookmarkBook = await _libraryContext.BookmarkBook
                .Where(x => x.UserId == userId && x.BookId == bookId)
                .Include(x => x.Book)
                .ToListAsync();

            return bookmarkBook.Count == 0? false: true;
        }

        [Authorize]
        [HttpGet("bookmarks")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBookmarkBooks()
        {
            var userId = this.userContextService.GetCurrentUserId();
            var bookmarkBooks = await _libraryContext.BookmarkBook
                .Where(x => x.UserId == userId)
                .Include(x => x.Book)
                .Include(x => x.Book.Category)
                .Select(x => x.Book)
                .ToListAsync();

            return Ok(bookmarkBooks);
        }

        [HttpPost("addbookmark")]
        public async Task<ActionResult> AddBook(BookmarkBookRequestDTO bookmarkBookDTO)
        {
            if (ModelState.IsValid)
            {
                var userId = this.userContextService.GetCurrentUserId();
                var user = await GetUserWithBookmarks(userId);

                var book = await _libraryContext.Book.FirstOrDefaultAsync(b => b.BookId == bookmarkBookDTO.BookId);

                if (book == null) return NotFound();
                

                var bookmarBook = await _libraryContext.BookmarkBook.FirstOrDefaultAsync(b => b.BookId == book.BookId && b.UserId == userId);

                if (bookmarBook != null) return BadRequest("You already added it to bookmarks");
                
                book.NumberOfBorrows++; 
                _libraryContext.Entry(book).State = EntityState.Modified;

                bookmarBook = new BookmarkBook
                {
                    BookId = book.BookId,
                    UserId = userId,
                };

                user.BookmarkBooks.Add(bookmarBook);

                await _libraryContext.SaveChangesAsync();
                return Ok();
            }

            var message = GetModelValidationErrors();

            return BadRequest(message);
        }


        [HttpDelete("deletebookmark")]
        public async Task<ActionResult> DeleteBookmarkBook(int bookId)
        {
            var userId = this.userContextService.GetCurrentUserId();

            var book = await _libraryContext.Book.FirstOrDefaultAsync(b => b.BookId == bookId);
            if (book != null)
            {
                book.NumberOfBorrows--;
                _libraryContext.Entry(book).State = EntityState.Modified;
            }

            var bookmark = await _libraryContext.BookmarkBook.FirstOrDefaultAsync(l => l.BookId == bookId && l.UserId == userId);

            if (bookmark == default) return NotFound();
            

            _libraryContext.BookmarkBook.Remove(bookmark);

            await _libraryContext.SaveChangesAsync();

            return Ok();
        }

        public async Task<User> GetUserWithBookmarks(int userId)
        {
            return await _libraryContext.Users
                .Include(u => u.BookmarkBooks)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        private bool BookExists(int id)
        {
            return _libraryContext.Book.Any(e => e.BookId == id);
        }

        private IEnumerable<string> GetModelValidationErrors()
        {
            return ModelState.Values
                 .SelectMany(v => v.Errors)
                 .Select(e => e.ErrorMessage);
        }
    }
}
