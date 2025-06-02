import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { UserDTO } from '../../dto/userDTO.model';
import { Observable } from 'rxjs';

@Injectable()
export class CreateBorrowingService {
  constructor(private http: HttpClient, private toastr: ToastrService) {}

  getUsers() {
    return this.http.get<UserDTO[]>('https://localhost:5001/api/users');
  }

  checkMembership(userId: number): Observable<boolean> {
    console.log(userId);
    return this.http.get<boolean>(
      `https://localhost:5001/api/users/ismember/${userId}`
    );
  }

  searchBooksByTitle(title: string): Observable<any[]> {
    return this.http.get<any[]>(
      `https://localhost:5001/api/book/search/${title}`
    );
  }

  getAvailabilityInfo(bookId: number): Observable<BookAvailability> {
    return this.http.get<BookAvailability>(
      `https://localhost:5001/api/book/availablecopies/${bookId}`
    );
  }

  borrowBook(data: { userId: number; bookId: number }): Observable<any> {
    return this.http.post(
      `https://localhost:5001/api/borrowsbook/addborrows`,
      data
    );
  }

  borrowFromReservation(data: {
    userId: number;
    bookCopyId: number;
  }): Observable<any> {
    return this.http.post(
      `https://localhost:5001/api/borrowsbook/addborrowfromreservation`,
      data
    );
  }
}

export interface BookAvailability {
  totalCopies: number;
  availableCopies: number;
  isFullyOccupied: boolean;
  soonestAvailableDate: string | null;
}
