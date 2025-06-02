using Library.Entities;
using Library.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Library.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public UserContextService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public int GetCurrentUserId()
        {
            var user = this.httpContextAccessor.HttpContext?.User;
            if (user == null || !(user.Identity?.IsAuthenticated ?? false))
            {
                throw new InvalidOperationException("User is not authenticated.");
            }

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim))
            {
                throw new InvalidOperationException("User ID is not available.");
            }

            return int.Parse(userIdClaim);
        }

        public async Task<string?> GetEmailByUserIdAsync(int userId)
        {
            var user = await this.userManager.FindByIdAsync(userId.ToString());
            return user?.Email;
        }
    }
}
