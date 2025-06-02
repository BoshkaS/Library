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
    public class PublishingHouseController : Controller
    {
        private LibraryContext _libraryContext;
        public PublishingHouseController(LibraryContext context)
        {
            _libraryContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublishingHouse>>> GetPublishingHouses()
        {
            return await _libraryContext.PublishingHouse.ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<PublishingHouse>> GetPublishingHouse(int id)
        {
            return await _libraryContext.PublishingHouse.FirstOrDefaultAsync(l => l.PublishingHouseId == id);
        }

        [HttpPost]
        public async Task<ActionResult> AddPublishingHouse(PublishingHouse publishingHouse)
        {
            if (ModelState.IsValid)
            {
                await _libraryContext.PublishingHouse.AddAsync(publishingHouse);
                await _libraryContext.SaveChangesAsync();

                return Ok();
            }

            var message = GetModelValidationErrors();

            return BadRequest(message);
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePublishingHouse(PublishingHouse publishingHouse)
        {
            if (publishingHouse.PublishingHouseId == default || !PublishingHouseExists(publishingHouse.PublishingHouseId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            _libraryContext.PublishingHouse.Update(publishingHouse);
            await _libraryContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePublishingHouse(int id)
        {
            var publishingHouse = await _libraryContext.PublishingHouse.FirstOrDefaultAsync(l => l.PublishingHouseId == id);

            if (publishingHouse == default) return NotFound();

            _libraryContext.PublishingHouse.Remove(publishingHouse);

            await _libraryContext.SaveChangesAsync();

            return Ok();
        }

        private bool PublishingHouseExists(int id)
        {
            return _libraryContext.PublishingHouse.Any(e => e.PublishingHouseId == id);
        }

        private IEnumerable<string> GetModelValidationErrors()
        {
            return ModelState.Values
                 .SelectMany(v => v.Errors)
                 .Select(e => e.ErrorMessage);
        }
    }
}
