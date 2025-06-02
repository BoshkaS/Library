import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserRatingLog } from '../../dto/userRatingLogDTO.model';

@Injectable({ providedIn: 'root' })
export class RatingLogService {
  constructor(private http: HttpClient) {}

  getLogsByUser(userId: number): Observable<UserRatingLog[]> {
    return this.http.get<UserRatingLog[]>(
      `https://localhost:5001/api/ratinglogs/user/${userId}`
    );
  }
}
