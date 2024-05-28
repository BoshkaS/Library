using Library_kursova.Data;
using Library_kursova.DTO;
using Library_kursova.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_kursova.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        private LibraryContext _libraryContext;

        public CommentController(LibraryContext context)
        {
            _libraryContext = context;
        }

        [HttpPost]
        public async Task<ActionResult<CommentResponseDTO>> AddComment(CommentRequestDTO commentDTO)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment
                {
                    Text = commentDTO.Text,
                    CreatedDate = DateTime.UtcNow,
                    UserId = await GetUserIdByEmailAsync(commentDTO.Email),
                    BookId = commentDTO.BookId
                };
                await _libraryContext.AddAsync(comment);
                await _libraryContext.SaveChangesAsync();

                var book = await _libraryContext.Book.FirstOrDefaultAsync(b => b.BookId == commentDTO.BookId);
                if (book != null)
                {
                    book.Comments.Add(comment);
                    book.NumberOfComments++; // Increment NumberOfComments
                    _libraryContext.Entry(book).State = EntityState.Modified;
                    await _libraryContext.SaveChangesAsync();
                }

                var commentResponseDTO = await _libraryContext.Comment
                    .Where(b => b.BookId == comment.BookId)
                    .Include(c => c.User)
                    .Select(c => new CommentResponseDTO
                    {
                        NickName = c.User.Nickname,
                        UserImage = c.User.UserImage,
                        Text = c.Text,
                        CreatedDate = c.CreatedDate
                    })
                    .FirstOrDefaultAsync();

                return commentResponseDTO;
            }
            var error = GetModelValidationErrors();

            return BadRequest(error);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteСomment(int id)
        {
            var comment = await _libraryContext.Comment.FirstOrDefaultAsync(l => l.CommentId == id);

            if (comment == default) return NotFound();

            var book = await _libraryContext.Book.FirstOrDefaultAsync(b => b.BookId == comment.BookId);
            if (book != null)
            {
                book.Comments.Remove(comment);
                book.NumberOfComments--; // Increment NumberOfComments
                _libraryContext.Entry(book).State = EntityState.Modified;
                await _libraryContext.SaveChangesAsync();
            }

            _libraryContext.Comment.Remove(comment);
            await _libraryContext.SaveChangesAsync();

            return Ok();
        }

        public async Task<int> GetUserIdByEmailAsync(string email)
        {
            var user = await _libraryContext.Users
                .FirstOrDefaultAsync(a => a.Email == email);

            if (user != null)
            {
                return user.Id;
            }
            else
            {
                // Handle the case where the author is not found
                return -1; // Or throw an exception, return null, etc.
            }
        }

        private IEnumerable<string> GetModelValidationErrors()
        {
            return ModelState.Values
                 .SelectMany(v => v.Errors)
                 .Select(e => e.ErrorMessage);
        }
    }
}
