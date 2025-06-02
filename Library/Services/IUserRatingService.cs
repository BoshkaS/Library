namespace Library.Services
{
    public interface IUserRatingService
    {
        Task AdjustRatingAsync(int userId, double delta, string reason);
        Task PenalizeLateReturn(int userId, int daysLate);
        Task RewardForGoodReturn(int userId);
        Task PenalizeForDamagedBook(int userId);
        Task PenalizeForCommentViolation(int userId);
        Task PenalizeLateReservationCancellation(int userId, DateTime reservedAt);
        Task PenalizeExpiredReservation(int userId);
    }
}
