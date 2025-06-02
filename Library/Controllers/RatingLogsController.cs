using Library.Data;
using Library.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RatingLogsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public RatingLogsController(LibraryContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserRatingLog>>> GetAllLogs()
        {
            var logs = await _context.UserRatingLog
                .Include(log => log.User)
                .OrderByDescending(log => log.ChangedAt)
                .ToListAsync();

            return Ok(logs);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserRatingLog>>> GetLogsByUser(int userId)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
                return NotFound("User not found");

            var logs = await _context.UserRatingLog
                .Where(log => log.UserId == userId)
                .OrderByDescending(log => log.ChangedAt)
                .ToListAsync();

            return Ok(logs);
        }
    }
}
