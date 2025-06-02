export class Book {
  public bookId: number;
  public title: string;
  public description: string;
  public bookImage: string;
  public yearOfPublication: number;
  public numberOfBorrows: number;
  public numberOfComments: number;
  public numberOfLikes: number;
  public isbn: number;
  public createdDate: Date;

  constructor(
    id: number,
    title: string,
    description: string,
    bookImage: string,
    yearOfPublication: number,
    numberOfBorrows: number,
    numberOfComments: number,
    numberOfLikes: number,
    isbn: number,
    createdDate: Date
  ) {
    this.bookId = id;
    this.title = title;
    this.description = description;
    this.bookImage = bookImage;
    this.yearOfPublication = yearOfPublication;
    this.numberOfBorrows = numberOfBorrows;
    this.numberOfComments = numberOfComments;
    this.numberOfLikes = numberOfLikes;
    this.isbn = isbn;
    this.createdDate = createdDate;
  }
}
