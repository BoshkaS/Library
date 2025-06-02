import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BorrowRequestResponseDTO } from '../../dto/borrowRequestResponseDTO.model';
import { BorrowRequestDTO } from '../../dto/borrowRequestDTO.model';
import { catchError, tap, throwError } from 'rxjs';

@Injectable()
export class ReturnDateService {
  constructor(private readonly http: HttpClient) {}

  getPendingRequests() {
    return this.http.get<BorrowRequestResponseDTO[]>(
      'https://localhost:5001/api/borrow-request/pending-requests'
    );
  }

  processDatesRequst(borrowRequestDTO: BorrowRequestDTO) {
    console.log(borrowRequestDTO);
    return this.http.post<BorrowRequestDTO>(
      'https://localhost:5001/api/borrow-request/process-extension',
      borrowRequestDTO
    );
  }
}
