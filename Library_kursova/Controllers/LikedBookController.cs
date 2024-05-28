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
    public class LikedBookController : Controller
    {
        private LibraryContext _libraryContext;
        public LikedBookController(LibraryContext context)
        {
            _libraryContext = context;
        }

        [HttpGet("liked")]
        public async Task<ActionResult<bool>> GetLikedBook(int bookId, string email)
        {
            var userId = await GetUserIdByEmail(email);
            var likedBook = await _libraryContext.LikedBook
                .Where(x => x.UserId == userId && x.BookId == bookId)
                .Include(x => x.Book)
                .ToListAsync();

            return likedBook.Count == 0? false: true;
        }

        [HttpPost("addlike")]
        public async Task<ActionResult> AddLike(BookmarkBookRequestDTO likedBookDTO)
        {
            if (ModelState.IsValid)
            {
                var userId = await GetUserIdByEmail(likedBookDTO.Email);
                var user = await GetUserWithLikes(userId);

                var book = await _libraryContext.Book.FirstOrDefaultAsync(b => b.BookId == likedBookDTO.BookId);

                if (book == null) return NotFound();
                

                var likedBook = await _libraryContext.LikedBook.FirstOrDefaultAsync(b => b.BookId == book.BookId && b.UserId == userId);

                if (likedBook != null) return BadRequest("You already added it to bookmarks");
                
                book.NumberOfLikes++; 
                _libraryContext.Entry(book).State = EntityState.Modified;

                likedBook = new LikedBook
                {
                    BookId = book.BookId,
                    UserId = userId,
                };

                user.LikedBooks.Add(likedBook);

                await _libraryContext.SaveChangesAsync();
                return Ok();
            }

            var message = GetModelValidationErrors();

            return BadRequest(message);
        }


        [HttpDelete("deletelike")]
        public async Task<ActionResult> DeleteLike(int bookId, string email)
        {
            var userId = await GetUserIdByEmail(email);

            var book = await _libraryContext.Book.FirstOrDefaultAsync(b => b.BookId == bookId);
            if (book != null)
            {
                book.NumberOfLikes--;
                _libraryContext.Entry(book).State = EntityState.Modified;
            }

            var like = await _libraryContext.LikedBook.FirstOrDefaultAsync(l => l.BookId == bookId && l.UserId == userId);

            if (like == default) return NotFound();

            

            _libraryContext.LikedBook.Remove(like);

            await _libraryContext.SaveChangesAsync();

            return Ok();
        }

        public async Task<User> GetUserWithLikes(int userId)
        {
            return await _libraryContext.Users
                .Include(u => u.LikedBooks)
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
