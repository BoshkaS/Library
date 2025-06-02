import { Component, Input, OnInit } from '@angular/core';
import { UserDTO } from '../../dto/userDTO.model';
import {
  BookAvailability,
  CreateBorrowingService,
} from './create-borrowing.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-create-borrowing',
  templateUrl: './create-borrowing.component.html',
  styleUrl: './create-borrowing.component.css',
})
export class CreateBorrowingComponent implements OnInit {
  userId!: number;
  isMember = false;
  books: any[] = [];
  selectedBookId: number | null = null;
  searchTerm = '';
  availableCopies: number | null = null;
  error: string | null = null;
  success: string | null = null;
  availabilityInfo: BookAvailability | null = null;

  constructor(
    private borrowingService: CreateBorrowingService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.userId = +params['userId']; // перетворення в число
      if (!this.userId) {
        this.error = 'Користувач не вказаний.';
        return;
      }
      this.checkMembership();
    });
  }

  checkMembership() {
    this.borrowingService.checkMembership(this.userId).subscribe({
      next: (result) => (this.isMember = result),
      error: () => (this.error = 'Помилка при перевірці членства.'),
    });
  }

  goToSettings() {
    this.router.navigate(['profile/' + this.userId + '/settings']);
  }

  searchBooks() {
    this.borrowingService.searchBooksByTitle(this.searchTerm).subscribe({
      next: (books) => {
        this.books = books;
      },
      error: () => {
        this.error = 'Не вдалося знайти книги.';
      },
    });
  }

  selectBook(bookId: number) {
    this.selectedBookId = bookId;
    this.availabilityInfo = null;
    this.error = null;

    this.borrowingService.getAvailabilityInfo(bookId).subscribe({
      next: (data) => {
        this.availabilityInfo = data;
      },
      error: () => {
        this.error = 'Не вдалося перевірити доступні копії.';
      },
    });
  }

  borrow() {
    console.log(this.userId);
    console.log(this.selectedBookId);
    if (!this.userId || this.selectedBookId === null) return;

    this.borrowingService
      .borrowBook({
        userId: this.userId,
        bookId: this.selectedBookId,
      })
      .subscribe({
        next: (res) => {
          this.success = `Книгу успішно позичено! Ідентифікатор позики: ${res.borrowId}, копія книги №${res.bookCopyId}`;
          this.error = null;
        },
        error: (err) => {
          this.error = err.error?.message || 'Помилка при позиченні книги.';
          this.success = null;
        },
      });
  }
}
