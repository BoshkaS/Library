using System.Security.Cryptography;
using System.Text;
using Library_kursova.Data;
using Library_kursova.DTO;
using Library_kursova.Entities;
using Library_kursova.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_kursova.Controllers
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
            if (await UserExists(registerDTO.Email)) return BadRequest("User with that email address already exists");

            var user = new User
            {
                Nickname = registerDTO.Nickname,
                Email = registerDTO.Email.ToLower(),
                CreatedDate = DateTime.UtcNow,
                UserImage = "https://res.cloudinary.com/dzwmwjg5u/image/upload/v1716810023/library-users/account_ch9eft.png",
                UserName = registerDTO.Nickname
               
            };

            var result = await userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await userManager.AddToRoleAsync(user, "user");

            if(!roleResult.Succeeded) return BadRequest(roleResult.Errors);

            return new RegisterDTO
            {
                Nickname = user.Nickname,
                Email = user.Email,
                UserImage = user.UserImage,
                Token = await tokenService.CreateToken(user)
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
                Nickname = user.Nickname,
                Email = user.Email,
                UserImage = user.UserImage,
                Token = await tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string email)
        {
            return await userManager.Users.AnyAsync(u => u.Email == email.ToLower());
        }
    }
}
