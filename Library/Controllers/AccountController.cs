using System.Security.Cryptography;
using System.Text;
using Library.Data;
using Library.DTO;
using Library.Entities;
using Library.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenService tokenService;

        public AccountController(UserManager<User> userManager, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        [HttpPost("register")] 
        public async Task<ActionResult<RegisterDTO>> Register(RegisterDTO registerDTO)
        {
            if (await UserExistsByEmail(registerDTO.Email)) return BadRequest("User with that email address already exists");
            if (await UserExistsByPhoneNumber(registerDTO.Email)) return BadRequest("User with that phone number already exists");

            var user = new User
            {
                Nickname = registerDTO.Nickname,
                Email = registerDTO.Email.ToLower(),
                CreatedDate = DateTime.UtcNow,
                UserImage = "https://res.cloudinary.com/dzwmwjg5u/image/upload/v1716810023/library-users/pkoze8ppvj8drpdzfxqy.png",
                UserName = registerDTO.Nickname,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                PhoneNumber = registerDTO.PhoneNumber,
                IsMember = false,
            };

            var result = await userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await userManager.AddToRoleAsync(user, "user");

            if(!roleResult.Succeeded) return BadRequest(roleResult.Errors);

            return new RegisterDTO
            {
                Id = user.Id,
                Nickname = user.Nickname,
                Email = user.Email,
                UserImage = user.UserImage,
                Token = await tokenService.CreateToken(user),
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                IsMember = false,
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<RegisterDTO>> Login(LoginDTO loginDTO)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.Email == loginDTO.Email);

            if (user == null) return Unauthorized("Invalid email");

            var result = await userManager.CheckPasswordAsync(user, loginDTO.Password);

            if (!result) return Unauthorized("Invalid password");

            return new RegisterDTO
            {
                Id = user.Id,
                Nickname = user.Nickname,
                Email = user.Email,
                UserImage = user.UserImage,
                Token = await tokenService.CreateToken(user),
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
            };
        }

        private async Task<bool> UserExistsByEmail(string email)
        {
            return await userManager.Users.AnyAsync(u => u.Email == email.ToLower());
        }

        private async Task<bool> UserExistsByPhoneNumber(string phone)
        {
            return await userManager.Users.AnyAsync(u => u.PhoneNumber == phone.ToLower());
        }
    }
}
