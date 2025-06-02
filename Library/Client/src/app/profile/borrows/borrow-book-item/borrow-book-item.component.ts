import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { BorrowBookResponseDTO } from '../../../dto/borrowBookResponseDTO.model';
import { Router } from '@angular/router';
import { AccountService } from '../../../auth/sign-in/account.service';
import { UserDTO } from '../../../dto/userDTO.model';
import { take } from 'rxjs';

@Component({
  selector: 'app-borrow-book-item',
  templateUrl: './borrow-book-item.component.html',
  styleUrl: './borrow-book-item.component.css',
})
export class BorrowBookItemComponent implements OnInit {
  @Input() borrowBook: BorrowBookResponseDTO;
  isModalContinueOpened = false;
  isModalReturnOpened = false;
  currentUser: UserDTO | null = null;
  isAdmin = false;

  @ViewChild('contextMenu') contextMenu: ElementRef;

  constructor(private router: Router, private accountService: AccountService) {}

  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: (userDTO) => (this.currentUser = userDTO),
    });

    this.isAdmin = this.currentUser?.roles.includes('admin') ?? false;
  }

  formatDate(date: Date, label: string): string {
    const months = [
      'січня',
      'лютого',
      'березня',
      'квітня',
      'травня',
      'червня',
      'липня',
      'серпня',
      'вересня',
      'жовтня',
      'листопада',
      'грудня',
    ];
    const d = new Date(date);
    return `${label}: ${d.getDate()} ${
      months[d.getMonth()]
    } ${d.getFullYear()}`;
  }

  returnBook() {}

  getDaysLeft(returnDate: Date): string {
    const now = new Date();
    const returnTime = new Date(returnDate).getTime();
    const diff = Math.ceil(
      (returnTime - now.getTime()) / (1000 * 60 * 60 * 24)
    );

    if (diff > 0) {
      return `Залишилось: ${diff} дн.`;
    } else if (diff === 0) {
      return `Останній день!`;
    } else {
      return `Прострочено на ${Math.abs(diff)} дн.`;
    }
  }

  navigateToBookDetails() {
    this.router.navigate([`book/${this.borrowBook.bookId}`]);
  }

  handleOpenContinueModal() {
    this.contextMenu.nativeElement.blur();
    this.isModalContinueOpened = true;
  }

  handleContinueClose() {
    this.isModalContinueOpened = false;
  }

  handleOpenReturnModel() {
    this.contextMenu.nativeElement.blur();
    this.isModalReturnOpened = true;
  }

  handleReturnClose() {
    this.isModalReturnOpened = false;
  }
}
