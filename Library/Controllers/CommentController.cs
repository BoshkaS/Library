using Library.Data;
using Library.DTO;
using Library.Entities;
using Library.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        private LibraryContext _libraryContext;
        private IUserContextService userContextService;

        public CommentController(LibraryContext context, IUserContextService userContextService)
        {
            _libraryContext = context;
            this.userContextService = userContextService;
        }

        [HttpPost]
        public async Task<ActionResult<CommentResponseDTO>> AddComment(CommentRequestDTO commentDTO)
        {
            var userId = this.userContextService.GetCurrentUserId();
            if (ModelState.IsValid)
            {
                var comment = new Comment
                {
                    Text = commentDTO.Text,
                    CreatedDate = DateTime.UtcNow,
                    UserId = userId,
                    BookId = commentDTO.BookId
                };
                await _libraryContext.AddAsync(comment);
                await _libraryContext.SaveChangesAsync();

                var book = await _libraryContext.Book.FirstOrDefaultAsync(b => b.BookId == commentDTO.BookId);
                if (book != null)
                {
                    book.Comments.Add(comment);
                    book.NumberOfComments++;
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
                        CreatedDate = c.CreatedDate,
                        UserId = c.UserId,
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
                book.NumberOfComments--;
                _libraryContext.Entry(book).State = EntityState.Modified;
                await _libraryContext.SaveChangesAsync();
            }

            _libraryContext.Comment.Remove(comment);
            await _libraryContext.SaveChangesAsync();

            return Ok();
        }

        private IEnumerable<string> GetModelValidationErrors()
        {
            return ModelState.Values
                 .SelectMany(v => v.Errors)
                 .Select(e => e.ErrorMessage);
        }
    }
}
