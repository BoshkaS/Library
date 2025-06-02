import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BookDetailService } from '../book-details.service';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../../auth/sign-in/account.service';
import { CreateReservationDTO } from '../../dto/createReservationDTO.model';

@Component({
  selector: 'app-reserve-book',
  templateUrl: './reserve-book.component.html',
  styleUrl: './reserve-book.component.css',
})
export class ReserveBookComponent {
  @Output() onSearchClose = new EventEmitter();
  @Input() id!: number;

  constructor(
    private bookDetailsService: BookDetailService,
    private toastr: ToastrService,
    private accountService: AccountService
  ) {}

  closeHandler() {
    this.onSearchClose.emit();
  }

  handleStopPropagation(event: MouseEvent) {
    event.stopPropagation();
  }

  onReserveBook() {
    this.accountService.currentUser$.subscribe((user) => {
      if (!user?.email) {
        this.toastr.error('Користувача не знайдено');
        return;
      }

      const dto: CreateReservationDTO = {
        bookId: this.id,
      };

      this.bookDetailsService.addReservation(dto).subscribe({
        next: (response) => {
          this.toastr.success('Книгу зарезервовано успішно');
          this.closeHandler();
        },
        error: (error) => {
          const message =
            error?.error?.message ||
            error?.error ||
            'Помилка під час резервування';
          this.toastr.error(message);
        },
      });
    });
  }
}
