import { Component, Input, OnInit } from '@angular/core';
import { UserRatingLog } from '../../dto/userRatingLogDTO.model';
import { RatingLogService } from './user-rating.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-rating-log',
  templateUrl: './user-rating-log.component.html',
  styleUrl: './user-rating-log.component.css',
})
export class UserRatingLogComponent implements OnInit {
  ratingLogs: UserRatingLog[] = [];
  loading = true;
  userId!: number;

  constructor(
    private route: ActivatedRoute,
    private ratingLogService: RatingLogService
  ) {}

  ngOnInit() {
    this.userId = +this.route.parent.snapshot.paramMap.get('id')!; // витягуємо :id з маршруту
    console.log(this.userId);
    if (this.userId) {
      this.ratingLogService.getLogsByUser(this.userId).subscribe({
        next: (logs) => (this.ratingLogs = logs),
        error: (err) => console.error(err),
        complete: () => (this.loading = false),
      });
    }
  }
}
