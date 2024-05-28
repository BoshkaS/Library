using Library_kursova.Data;
using Library_kursova.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_kursova.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private LibraryContext _libraryContext;
        public CategoryController(LibraryContext context)
        {
            _libraryContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _libraryContext.Category.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            return await _libraryContext.Category.FirstOrDefaultAsync(l => l.CategoryId == id);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory(Category сategory)
        {
            if (ModelState.IsValid)
            {
                await _libraryContext.Category.AddAsync(сategory);
                await _libraryContext.SaveChangesAsync();

                return Ok();
            }

            var message = GetModelValidationErrors();

            return BadRequest(message);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCategory(Category сategory)
        {
            if (сategory.CategoryId == default || !CategoryExists(сategory.CategoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            _libraryContext.Category.Update(сategory);
            await _libraryContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var сategory = await _libraryContext.Category.FirstOrDefaultAsync(l => l.CategoryId == id);

            if (сategory == default) return NotFound();

            _libraryContext.Category.Remove(сategory);

            await _libraryContext.SaveChangesAsync();

            return Ok();
        }

        private bool CategoryExists(int id)
        {
            return _libraryContext.Category.Any(e => e.CategoryId == id);
        }

        private IEnumerable<string> GetModelValidationErrors()
        {
            return ModelState.Values
                 .SelectMany(v => v.Errors)
                 .Select(e => e.ErrorMessage);
        }
    }
}
