import { BookRequest } from './models/bookRequest.model';

export class BookDTORequest {
  public book: BookRequest;
  public authorNames: string[];
  public language: string;
  public category: string;
  public publishingHouse: string;

  constructor(
    book: BookRequest,
    authorNames: string[],
    language: string,
    category: string,
    publishingHouse: string
  ) {
    this.book = book;
    this.authorNames = authorNames;
    this.language = language;
    this.category = category;
    this.publishingHouse = publishingHouse;
  }
}
