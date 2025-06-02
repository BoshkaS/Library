import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BorrowRequestResponseDTO } from '../../../dto/borrowRequestResponseDTO.model';
import { BorrowRequestDTO } from '../../../dto/borrowRequestDTO.model';
import { ReturnDateService } from '../process-return-date.service';
import { Toast, ToastrService } from 'ngx-toastr';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-return-date',
  templateUrl: './return-date.component.html',
  styleUrl: './return-date.component.css',
})
export class ReturnDateComponent {
  @Input() requestDate: BorrowRequestResponseDTO;

  @Output() requestProcessed = new EventEmitter<number>();

  model: BorrowRequestDTO = {
    requestId: 0,
    decision: '',
  };

  formattedReturnDate!: string;
  formattedRequestDate!: string;

  constructor(
    private returnDateService: ReturnDateService,
    private toastr: ToastrService,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    this.formattedReturnDate = this.formatDate(
      this.requestDate.currentReturnDate,
      false
    );
    this.formattedRequestDate = this.formatDate(
      this.requestDate.requestDate,
      true
    );
  }

  formatDate(dateString: Date, includeTime: boolean): string {
    const date = new Date(dateString);

    const datePart = new Intl.DateTimeFormat('uk-UA', {
      day: 'numeric',
      month: 'long',
      year: 'numeric',
    }).format(date);

    if (includeTime) {
      const timePart = date.toLocaleTimeString('uk-UA', {
        hour: '2-digit',
        minute: '2-digit',
      });

      return `${datePart} о ${timePart}`;
    }

    return `${datePart}`; // Додаємо "р." лише раз
  }

  acceptRequest() {
    this.processRequest('approve');
  }

  rejectRequest() {
    this.processRequest('reject');
  }

  private processRequest(decision: 'approve' | 'reject') {
    console.log(
      `Sending request to process: ID ${this.requestDate.requestId}, Decision: ${decision}`
    );
    const model = {
      decision: decision,
      requestId: this.requestDate.requestId,
    };

    this.http
      .post<string>(
        'https://localhost:5001/api/borrow-request/process-extension',
        model,
        { responseType: 'text' as 'json' }
      )
      .subscribe({
        next: (response) => {
          this.toastr.success(
            decision === 'approve'
              ? 'Запит успішно схвалено!'
              : 'Запит успішно відхилено!'
          );
          // Emit event to remove the request from the list
          this.requestProcessed.emit(this.requestDate.requestId);
        },
        error: (error) => console.log(error),
      });
  }
}
