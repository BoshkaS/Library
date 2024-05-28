import { CommentResponseDTO } from "./commentResponseDTO.model";
import { Book } from "./models/book.model";
import { Category } from "./models/category.model";
import { Language } from "./models/language.model";
import { PublishingHouse } from "./models/publising-house.model";

export class BookDTO {
    public book: Book;
    public authorNames: string[];
    public language: string;
    public category: string;
    public publishingHouse: string;
    public comments: CommentResponseDTO[]

    constructor(
        book: Book,
        authorNames: string[],
        language: string,
        category: string,
        publishingHouse: string,
        comments: CommentResponseDTO[]
    ) {
        this.book = book;
        this.authorNames = authorNames;
        this.language = language;
        this.category = category;
        this.publishingHouse = publishingHouse;
        this.comments = comments;
    }
}