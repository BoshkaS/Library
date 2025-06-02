export class ReservationDTO {
  reservationId: number;
  bookCopyId: number;
  bookTitle: string;
  bookImage: string;
  reservedAt: Date;
  expiresAt: Date;
  status: string;
  bookId: number;
  isActive: boolean;

  constructor(
    reservationId: number,
    bookCopyId: number,
    bookTitle: string,
    bookImage: string,
    reservedAt: Date,
    expiresAt: Date,
    status: string,
    bookId: number,
    isActive: boolean
  ) {
    this.reservationId = reservationId;
    this.bookCopyId = bookCopyId;
    this.bookTitle = bookTitle;
    this.reservedAt = new Date(reservedAt);
    this.expiresAt = new Date(expiresAt);
    this.status = status;
    this.bookImage = bookImage;
    this.bookId = bookId;
    this.isActive = isActive;
  }
}
