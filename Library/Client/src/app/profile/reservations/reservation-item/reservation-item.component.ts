import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { ReservationDTO } from '../../../dto/reservationDTO.model';
import { UserDTO } from '../../../dto/userDTO.model';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../auth/sign-in/account.service';
import { take } from 'rxjs';
import { ReservationService } from '../reservations.service';
import { ToastrService } from 'ngx-toastr';
import { CreateBorrowingService } from '../../../admin/create-borrowing/create-borrowing.service';

@Component({
  selector: 'app-reservation-item',
  templateUrl: './reservation-item.component.html',
  styleUrl: './reservation-item.component.css',
})
export class ReservationItemComponent implements OnInit {
  @Input() reservedBook: ReservationDTO;

  currentUser: UserDTO | null = null;
  isAdmin = false;
  isModalConfirmOpened = false;
  userIdPage: number;

  @ViewChild('contextMenu') contextMenu: ElementRef;

  constructor(
    private router: Router,
    private accountService: AccountService,
    private reservationService: ReservationService,
    private toastr: ToastrService,
    private createBorrowingService: CreateBorrowingService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: (userDTO) => (this.currentUser = userDTO),
    });

    this.isAdmin = this.currentUser?.roles.includes('admin') ?? false;

    this.route.parent?.params.subscribe((params) => {
      this.userIdPage = +params['id'];
    });
  }

  getCountdown(): string {
    const now = new Date().getTime();
    const expires = new Date(this.reservedBook.expiresAt).getTime();
    const diff = expires - now;

    if (diff <= 0) return 'Завершено';

    const days = Math.floor(diff / (1000 * 60 * 60 * 24));
    const hours = Math.floor((diff / (1000 * 60 * 60)) % 24);
    const minutes = Math.floor((diff / (1000 * 60)) % 60);

    return `${days}д ${hours}г ${minutes}хв`;
  }

  navigateToBookDetails() {
    this.router.navigate([`book/${this.reservedBook.bookId}`]);
  }

  cancelReservation() {
    this.reservationService
      .cancelReservation(this.reservedBook.reservationId)
      .subscribe({
        next: () => {
          this.reservedBook.isActive = false; // reflect UI change
          this.toastr.success('Резервацію успішно скасовано.');
        },
        error: (err) => {
          console.error(err);
          this.toastr.error('Не вдалося скасувати резервацію.');
        },
      });
  }

  handleOpenConfirmModal() {
    this.contextMenu.nativeElement.blur();
    this.isModalConfirmOpened = true;
  }

  handleConfirmClose() {
    this.isModalConfirmOpened = false;
  }

  approveReservation() {
    this.createBorrowingService
      .borrowFromReservation({
        bookCopyId: this.reservedBook.bookCopyId,
        userId: this.userIdPage,
      })
      .subscribe({
        next: () => {
          // Cancel reservation after borrow is successful
          this.reservationService
            .cancelReservation(this.reservedBook.reservationId)
            .subscribe({
              next: () => {
                this.reservedBook.isActive = false;
                this.toastr.success('Книгу видано. Резервацію скасовано.');
              },
              error: (cancelError) => {
                console.error(cancelError);
                this.toastr.warning(
                  'Книгу видано, але резервацію не вдалося скасувати.'
                );
              },
            });
        },
        error: (err) => {
          console.error(err);
          this.toastr.error('Не вдалося підтвердити резервацію.');
        },
      });
  }
}
