import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BookDTO } from '../dto/bookDTO.model';

@Injectable()
export class SearchBookService {
  constructor(private http: HttpClient) {}

  getSearchBook(searchString: string) {
    return this.http.get<BookDTO[]>('https://localhost:5001/api/book/search/'+ searchString);
  }
}
