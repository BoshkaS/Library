export class BorrowBookResponseDTO {
  public borrowsBookId: number;
  public bookId: number;
  public title: string;
  public bookImage: string;
  public borrowDate: Date;
  public returnDate: Date;
  public isReturned: boolean;
  public actualReturnDate: Date;

  constructor(
    borrowsBookId: number,
    bookId: number,
    title: string,
    bookImage: string,
    borrowDate: Date,
    returnDate: Date,
    isReturned: boolean,
    actualReturnDate: Date
  ) {
    this.borrowsBookId = borrowsBookId;
    this.bookId = bookId;
    this.title = title;
    this.bookImage = bookImage;
    this.borrowDate = borrowDate;
    this.returnDate = returnDate;
    this.isReturned = isReturned;
    this.actualReturnDate = actualReturnDate;
  }
}
