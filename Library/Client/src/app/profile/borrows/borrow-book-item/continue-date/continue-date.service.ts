import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ContinueDateService {
  constructor(private http: HttpClient) {}

  continueDate(borrowId: number) {
    return this.http.post(
      'https://localhost:5001/api/borrow-request/request-extension/' + borrowId,
      borrowId
    );
  }
}
