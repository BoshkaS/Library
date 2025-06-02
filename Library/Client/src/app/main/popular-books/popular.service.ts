import { HttpClient } from "@angular/common/http";
import { BookDTO } from "../../dto/bookDTO.model";
import { Injectable } from "@angular/core";

@Injectable()
export class PopularBooksService {

    constructor(private http: HttpClient) {}
    popular_books =[
        {
            img: '../../assets/images/icon_L.png',
            name: 'Вінланд Сага',
            author: 'Австор',
            type: 'Художня книга',
            genre: 'Любовний роман',
            bookmarks: 3,
            borrows: 4,
            reviews: 1,
            likes: 10
        },
        {
            img: '../../assets/images/icon_L.png',
            name: 'Вінланд Сага',
            author: 'Австор',
            type: 'Художня книга',
            genre: 'Любовний роман',
            bookmarks: 3,
            borrows: 4,
            reviews: 1,
            likes: 10
        },
        {
            img: '../../assets/images/icon_L.png',
            name: 'Вінланд Сага',
            author: 'Австор',
            type: 'Художня книга',
            genre: 'Любовний роман',
            bookmarks: 3,
            borrows: 4,
            reviews: 1,
            likes: 10
        },
        {
            img: '../../assets/images/icon_L.png',
            name: 'Вінланд Сага',
            author: 'Австор',
            type: 'Художня книга',
            genre: 'Любовний роман',
            bookmarks: 3,
            borrows: 4,
            reviews: 1,
            likes: 10
        }
    ]

    getPopularBook() {
        return this.http.get<BookDTO[]>('https://localhost:5001/api/book/popular')
    }
}