using Library.Data;
using Library.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : Controller
    {
        private LibraryContext _libraryContext;
        public AuthorController(LibraryContext context)
        {
            _libraryContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return await _libraryContext.Author.ToListAsync();
        }

        [Authorize(Roles = "admin,user")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            return await _libraryContext.Author.FirstOrDefaultAsync(l => l.AuthorId == id);
        }

        //[Authorize(Policy = "RequiredAdminRole")]
        [HttpPost]
        public async Task<ActionResult> AddAuthor(Author author)
        {
            if (ModelState.IsValid)
            {
                await _libraryContext.Author.AddAsync(author);
                await _libraryContext.SaveChangesAsync();

                return Ok();
            }

            var message = GetModelValidationErrors();

            return BadRequest(message);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAuthor(Author author)
        {
            if (author.AuthorId == default || !AuthorExists(author.AuthorId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            _libraryContext.Author.Update(author);
            await _libraryContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var author = await _libraryContext.Author.FirstOrDefaultAsync(l => l.AuthorId == id);

            if (author == default) return NotFound();

            _libraryContext.Author.Remove(author);

            await _libraryContext.SaveChangesAsync();

            return Ok();
        }

        private bool AuthorExists(int id)
        {
            return _libraryContext.Author.Any(e => e.AuthorId == id);
        }

        private IEnumerable<string> GetModelValidationErrors()
        {
            return ModelState.Values
                 .SelectMany(v => v.Errors)
                 .Select(e => e.ErrorMessage);
        }
    }
}
