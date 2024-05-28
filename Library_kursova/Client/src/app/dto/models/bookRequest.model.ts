export class BookRequest {
  public title: string;
  public description: string;
  public yearOfPublication: number;
  public numberOfBorrows: number;
  public numberOfComments: number;
  public numberOfLikes: number;

  constructor(
    title: string,
    description: string,
    year: number,
    numberOfBorrows: number,
    numberOfComments: number,
    numberOfLikes: number
  ) {
    this.title = title;
    this.description = description;
    this.yearOfPublication = year;
    this.numberOfBorrows = numberOfBorrows;
    this.numberOfComments = numberOfComments;
    this.numberOfLikes = numberOfLikes;
  }
}
