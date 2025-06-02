export class CommentRequestDTO {
  public text: string;
  public bookId: number;

  constructor(text: string, bookId: number) {
    this.text = text;
    this.bookId = bookId;
  }
}
