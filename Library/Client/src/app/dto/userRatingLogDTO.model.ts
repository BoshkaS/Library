export interface UserRatingLog {
  userRatingId: number;
  userId: number;
  changedAt: string; // або Date, якщо парсиш
  changeAmount: number;
  reason: string;
  ratingAfterChange: number;
}
