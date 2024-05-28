export class CommentRequestDTO {
  public email: string;
  public text: string;
  public bookId: number;

  constructor(email: string, text: string, bookId: number) {
    this.email = email;
    this.text = text;
    this.bookId = bookId;
  }
}
