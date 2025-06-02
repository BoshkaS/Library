export class BorrowRequestResponseDTO {
  requestId: number;
  borrowsBookId: number;
  userEmail: string; // User who made the request
  bookTitle: string; // Title of the borrowed book
  bookCopyId: number; // Book copy ID
  currentReturnDate: Date; // DateOnly in C# → string in TypeScript
  requestDate: Date; // DateTime in C# → string in TypeScript
  approved: boolean | null; // NULL = pending, true = approved, false = rejected

  constructor(
    requestId: number,
    borrowsBooksId: number,
    userEmail: string,
    bookTitle: string,
    bookCopyId: number,
    currentReturnDate: Date,
    requestDate: Date,
    approved: boolean | null
  ) {
    this.requestId = requestId;
    this.borrowsBookId = borrowsBooksId;
    this.userEmail = userEmail;
    this.bookTitle = bookTitle;
    this.bookCopyId = bookCopyId;
    this.currentReturnDate = currentReturnDate;
    this.requestDate = requestDate;
    this.approved = approved;
  }
}
