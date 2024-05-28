import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BookDTO } from "../../dto/bookDTO.model";

@Injectable()
export class RandomBookService {
    constructor(private http: HttpClient){}

    getRandomBook() {
        return this.http.get<BookDTO>('https://localhost:5001/api/book/random');
    }
}