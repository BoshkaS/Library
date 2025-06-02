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
    public class LikedBookController : Controller
    {
        private LibraryContext _libraryContext;
        private IUserContextService _userContextService;
        public LikedBookController(LibraryContext context, IUserContextService userContextService)
        {
            _libraryContext = context;
            _userContextService = userContextService;
        }

        [HttpGet("liked")]
        public async Task<ActionResult<bool>> GetLikedBook(int bookId)
        {
            var userId = this._userContextService.GetCurrentUserId();
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
                var userId = this._userContextService.GetCurrentUserId();
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
        public async Task<ActionResult> DeleteLike(int bookId)
        {
            var userId = this._userContextService.GetCurrentUserId();

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

        private IEnumerable<string> GetModelValidationErrors()
        {
            return ModelState.Values
                 .SelectMany(v => v.Errors)
                 .Select(e => e.ErrorMessage);
        }
    }

    
}
