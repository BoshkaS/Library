using System.Security.Claims;
using API.Extensions;
using Library_kursova.Data;
using Library_kursova.DTO;
using Library_kursova.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_kursova.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookmarkBookController : Controller
    {
        private LibraryContext _libraryContext;
        public BookmarkBookController(LibraryContext context)
        {
            _libraryContext = context;
        }

        [HttpGet("bookmark")]
        public async Task<ActionResult<bool>> GetBookmarkBook(int bookId, string email)
        {
            var userId = await GetUserIdByEmail(email);
            var bookmarkBook = await _libraryContext.BookmarkBook
                .Where(x => x.UserId == userId && x.BookId == bookId)
                .Include(x => x.Book)
                .ToListAsync();

            return bookmarkBook.Count == 0? false: true;
        }

        [HttpGet("bookmarks")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBookmarkBooks(string email)
        {
            var userId = await GetUserIdByEmail(email);
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
                var userId = await GetUserIdByEmail(bookmarkBookDTO.Email);
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
        public async Task<ActionResult> DeleteBook(int bookId, string email)
        {
            var userId = await GetUserIdByEmail(email);

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

        public async Task<int> GetUserIdByEmail(string email)
        {
            var user =  await _libraryContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user.Id;
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBook(Book book)
        {
            if (book.BookId == default || !BookExists(book.BookId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            _libraryContext.Book.Update(book);
            await _libraryContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await _libraryContext.Book.FirstOrDefaultAsync(l => l.BookId == id);

            if (book == default) return NotFound();

            _libraryContext.Book.Remove(book);

            await _libraryContext.SaveChangesAsync();

            return Ok();
        }

        public async Task<int> GetAuthorIdByNameAsync(string authorName)
        {
            var author = await _libraryContext.Author
                .FirstOrDefaultAsync(a => a.Pseudonym == authorName);

            if (author != null)
            {
                return author.AuthorId;
            }
            else
            {
                // Handle the case where the author is not found
                return -1; // Or throw an exception, return null, etc.
            }
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
