using Library.Entities;

namespace Library.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
