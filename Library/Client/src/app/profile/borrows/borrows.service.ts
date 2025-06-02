import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BorrowBookResponseDTO } from '../../dto/borrowBookResponseDTO.model';
import { Observable } from 'rxjs';

@Injectable()
export class BorrowsService {
  constructor(private http: HttpClient) {}

  getBorrowsBook() {
    return this.http.get<BorrowBookResponseDTO[]>(
      'https://localhost:5001/api/borrowsbook/all-borrows'
    );
  }

  returnBook(borrowId: number, condition: string): Observable<any> {
    const params = new HttpParams().set('condition', condition);
    return this.http.post(
      `https://localhost:5001/api/borrowsbook/return-book/${borrowId}`,
      {},
      { params }
    );
  }
}
