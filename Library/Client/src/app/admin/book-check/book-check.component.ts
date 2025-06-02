import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BookCheckService } from './book-check.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-book-check',
  templateUrl: './book-check.component.html',
  styleUrl: './book-check.component.css',
})
export class BookCheckComponent {
  constructor(
    private router: Router,
    private bookService: BookCheckService,
    private toastr: ToastrService
  ) {}

  checkBook(checkForm: any) {
    const { isbn } = checkForm.value;
    console.log(isbn);
    this.bookService.checkIfBookExists(isbn).subscribe({
      next: (bookExists) => {
        if (bookExists) {
          this.bookService.addCopy(isbn).subscribe(() => {
            this.toastr.success('Копію книги додано!');
            this.router.navigate(['/admin']);
          });
        } else {
          this.toastr.info('Книга не знайдена, заповніть повну інформацію');
          this.router.navigate(['create-book']);
        }
      },
      error: () => this.toastr.error('Помилка при перевірці'),
    });
  }
}
