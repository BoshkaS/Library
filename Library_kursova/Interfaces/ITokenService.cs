using Library_kursova.Entities;

namespace Library_kursova.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
