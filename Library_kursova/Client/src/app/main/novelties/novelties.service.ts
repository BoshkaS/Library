import { HttpClient } from '@angular/common/http';
import { Book } from '../../dto/models/book.model';
import { Injectable } from '@angular/core';

@Injectable()
export class NoveltiesItemService {
  constructor(private http: HttpClient) {}

  getNovelties() {
    return this.http.get<Book[]>('https://localhost:5001/api/book/novelties');
  }
}
