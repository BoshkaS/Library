import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReservationDTO } from '../../dto/reservationDTO.model';

@Injectable()
export class ReservationService {
  constructor(private http: HttpClient) {}

  getReservationBook(userId: number) {
    return this.http.get<ReservationDTO[]>(
      `https://localhost:5001/api/reservation/all-reservations/${userId}`
    );
  }

  cancelReservation(id: number) {
    return this.http.put(
      'https://localhost:5001/api/reservation/cancel-reservation/' + id,
      null
    );
  }
}
