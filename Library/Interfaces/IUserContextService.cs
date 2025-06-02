namespace Library.Interfaces
{
    public interface IUserContextService
    {
        int GetCurrentUserId();

        Task<string?> GetEmailByUserIdAsync(int userId);
    }
}
