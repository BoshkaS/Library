import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class DeleteBookService {
  constructor(private http: HttpClient) {}

  deleteBook(bookId: number) {
    return this.http.delete('https://localhost:5001/api/book/' + bookId);
  }

  deleteBookCopy(copyId: number) {
    return this.http.delete(`https://localhost:5001/api/book/copy/${copyId}`);
  }
}
