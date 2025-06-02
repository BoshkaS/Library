using Library.Data;
using Library.Entities;

namespace Library.Services
{
    public class UserRatingService : IUserRatingService
    {
        private readonly LibraryContext _context;

        public UserRatingService(LibraryContext context)
        {
            _context = context;
        }

        public async Task AdjustRatingAsync(int userId, double delta, string reason)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return;

            double newRating = Math.Clamp(user.Rating + delta, 0.0, 10.0);

            var log = new UserRatingLog
            {
                UserId = userId,
                ChangeAmount = delta,
                Reason = reason,
                RatingAfterChange = newRating
            };

            user.Rating = newRating;
            _context.UserRatingLog.Add(log);
            await _context.SaveChangesAsync();
        }

        public Task PenalizeLateReturn(int userId, int daysLate) =>
            AdjustRatingAsync(userId, -0.2 * daysLate, $"Прострочено повернення на {daysLate} днів");

        public Task RewardForGoodReturn(int userId) =>
            AdjustRatingAsync(userId, 0.2, "Книга повернута в хорошому стані");

        public Task PenalizeForDamagedBook(int userId) =>
            AdjustRatingAsync(userId, -1.0, "Повернено пошкоджену книгу");

        public Task PenalizeForCommentViolation(int userId) =>
            AdjustRatingAsync(userId, -0.5, "Коментар видалено через порушення правил");

        public Task PenalizeLateReservationCancellation(int userId, DateTime reservedAt)
        {
            if ((DateTime.UtcNow - reservedAt).TotalDays > 2)
            {
                return AdjustRatingAsync(userId, -0.3, "Скасування бронювання після 2 днів");
            }
            return Task.CompletedTask;
        }

        public Task PenalizeExpiredReservation(int userId)
        {
            return AdjustRatingAsync(userId, -0.4, "Бронювання не забрано — строк дії минув");
        }
    }
}
