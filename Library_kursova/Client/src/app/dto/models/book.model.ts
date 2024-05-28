export class Book {
  public bookId: number;
  public title: string;
  public description: string;
  public bookImage: string;
  public yearOfPublication: number;
  public numberOfBorrows: number;
  public numberOfComments: number;
  public numberOfLikes: number;

  constructor(
    id: number,
    title: string,
    description: string,
    bookImage: string,
    year: number,
    numberOfBorrows: number,
    numberOfComments: number,
    numberOfLikes: number
  ) {
    this.bookId = id;
    this.title = title;
    this.description = description;
    this.bookImage = bookImage;
    this.yearOfPublication = year;
    this.numberOfBorrows = numberOfBorrows;
    this.numberOfComments = numberOfComments;
    this.numberOfLikes = numberOfLikes;
  }
}
