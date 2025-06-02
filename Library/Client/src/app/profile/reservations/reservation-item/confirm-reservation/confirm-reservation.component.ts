import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ContinueDateService } from '../../../borrows/borrow-book-item/continue-date/continue-date.service';
import { ToastrService } from 'ngx-toastr';
import { ReservationService } from '../../reservations.service';

@Component({
  selector: 'app-confirm-reservation',
  templateUrl: './confirm-reservation.component.html',
  styleUrl: './confirm-reservation.component.css',
})
export class ConfirmReservationComponent {
  @Output() onSearchClose = new EventEmitter();
  @Input() id!: number;

  constructor(
    private reservationService: ReservationService,
    private toastr: ToastrService
  ) {}

  cancelReservation() {
    this.reservationService.cancelReservation(this.id).subscribe({
      next: () => {
        this.toastr.success('Резервацію успішно скасовано.');
      },
      error: (err) => {
        console.error(err);
        this.toastr.error('Не вдалося скасувати резервацію.');
      },
    });
  }
  closeHandler() {
    this.onSearchClose.emit();
  }

  handleStopPropagation(event: MouseEvent) {
    event.stopPropagation();
  }
}
