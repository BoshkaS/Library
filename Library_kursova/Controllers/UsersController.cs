using System.Security.Claims;
using API.Extensions;
using DevExpress.Internal;
using Library_kursova.Data;
using Library_kursova.DTO;
using Library_kursova.Entities;
using Library_kursova.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_kursova.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class UsersController : Controller
    {
        private readonly LibraryContext _libraryContext;
        private readonly IPhotoService _photoService;

        public UsersController(LibraryContext context, IPhotoService photoService)
        {
            _libraryContext = context;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _libraryContext.Users.ToListAsync();

            return users;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _libraryContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        
        [HttpPut]
        public async Task<ActionResult<RegisterDTO>> UpdateUser(UpdateUserDTO userDTO)
        {
            var user = await _libraryContext.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email);
            _libraryContext.Entry(user).State = EntityState.Modified;
            user.ModifiedDate = DateTime.UtcNow;

            user.Nickname = userDTO.Nickname;
            user.UserName = userDTO.Nickname;


            if (user.Id == default || !UserExists(user.Id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            _libraryContext.Users.Update(user);
            await _libraryContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("get-photo")]
        public async Task<ActionResult<string>> GetPhoto([FromQuery] string email)
        {
            var user = await _libraryContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user.Id == default || !UserExists(user.Id))
                return NotFound();

            return Content(user.UserImage, "text/plain");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<string>> AddPhoto(IFormFile file, [FromQuery]string email)
        {
            var user = await _libraryContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user.Id == default || !UserExists(user.Id))
                return NotFound();

            var result = await _photoService.AddPhotoAsync(/*photoRequestDTO.*/file, "user");

            if(result.Error != null) return BadRequest(result.Error);

            user.UserImage = result.SecureUrl.AbsoluteUri;
            _libraryContext.Users.Update(user);
            await _libraryContext.SaveChangesAsync();


            return Ok();
        }

        private bool UserExists(int id)
        {
            return _libraryContext.Users.Any(e => e.Id == id);
        }
    }
}
