import { Component, OnInit } from '@angular/core';
import { BorrowRequestResponseDTO } from '../../dto/borrowRequestResponseDTO.model';
import { ReturnDateService } from './process-return-date.service';

@Component({
  selector: 'app-process-return-date',
  templateUrl: './process-return-date.component.html',
  styleUrl: './process-return-date.component.css',
})
export class ProcessReturnDateComponent implements OnInit {
  requestsForReturning: BorrowRequestResponseDTO[] = [];

  constructor(private readonly returnDateService: ReturnDateService) {}

  ngOnInit(): void {
    this.returnDateService.getPendingRequests().subscribe({
      next: (requests) => {
        this.requestsForReturning = requests;
      },
      error: (error) => {
        console.error('Error fetching pending requests:', error);
      },
    });
  }

  removeRequest(requestId: number) {
    this.requestsForReturning = this.requestsForReturning.filter(
      (r) => r.requestId !== requestId
    );
  }
}
