import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class BookCheckService {
  constructor(private http: HttpClient) {}

  checkIfBookExists(isbn: string): Observable<boolean> {
    const params = new HttpParams().set('isbn', isbn);

    return this.http.get<boolean>(`https://localhost:5001/api/book/exists`, {
      params,
    });
  }

  addCopy(isbn: string): Observable<void> {
    return this.http.post<void>(
      `https://localhost:5001/api/book/add-copy`,
      JSON.stringify(isbn), // Обертаємо в JSON
      {
        headers: { 'Content-Type': 'application/json' },
      }
    );
  }
}
