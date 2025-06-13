using Library.Data;
using Library.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LanguageController : Controller
    {
        private LibraryContext _libraryContext;
        public LanguageController(LibraryContext context)
        {
            _libraryContext = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Language>>> GetLanguages()
        {
            return await _libraryContext.Language.ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Language>> GetLanguage(int id)
        {
            return await _libraryContext.Language.FirstOrDefaultAsync(l => l.LanguageId == id);
        }
        
        [HttpPost]
        public async Task<ActionResult> AddLanguage(Language language)
        {
            if (ModelState.IsValid)
            {
                await _libraryContext.Language.AddAsync(language);
                await _libraryContext.SaveChangesAsync();

                return Ok();
            }

            var message = GetModelValidationErrors();

            return BadRequest(message);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateLanguage(Language language)
        {
            if (language.LanguageId == default || !LanguageExists(language.LanguageId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            _libraryContext.Language.Update(language);
            await _libraryContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLanguage(int id)
        {
            var lang = await _libraryContext.Language.FirstOrDefaultAsync(l => l.LanguageId == id);

            if (lang == default) return NotFound();

            _libraryContext.Language.Remove(lang);

            await _libraryContext.SaveChangesAsync();

            return Ok();
        }

        private bool LanguageExists(int id)
        {
            return _libraryContext.Language.Any(e => e.LanguageId == id);
        }

        private IEnumerable<string> GetModelValidationErrors()
        {
            return ModelState.Values
                 .SelectMany(v => v.Errors)
                 .Select(e => e.ErrorMessage);
        }
    }
}
