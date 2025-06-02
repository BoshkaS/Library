using Microsoft.AspNetCore.Identity;

namespace Library.Entities
{
    public class AppUserRole : IdentityUserRole<int>
    {
        public User User { get; set; }

        public AppRole Role { get; set; }
    }
}
