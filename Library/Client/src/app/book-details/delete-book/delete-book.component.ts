import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DeleteBookService } from './delete-book.service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-delete-book',
  templateUrl: './delete-book.component.html',
  styleUrl: './delete-book.component.css',
})
export class DeleteBookComponent {
  @Output() onSearchClose = new EventEmitter();
  @Input() id!: number;

  isCopyListVisible = false;
  copies: any[] = [];

  constructor(
    private deleteBookService: DeleteBookService,
    private router: Router,
    private http: HttpClient
  ) {}

  deleteBook() {
    if (this.id) {
      this.deleteBookService.deleteBook(this.id).subscribe(() => {
        console.log('Book deleted successfully');
        // Close modal or navigate away
      });
    }
    this.router.navigate(['main']);
  }

  deleteBookCopy(copyId: number) {
    this.deleteBookService.deleteBookCopy(copyId).subscribe(() => {
      console.log(`Copy ${copyId} deleted`);
      this.copies = this.copies.filter((c) => c.bookCopyId !== copyId);
    });
  }

  showCopyList() {
    this.isCopyListVisible = true;
    this.http
      .get<any[]>(`https://localhost:5001/api/book/${this.id}/copies`)
      .subscribe((data) => {
        this.copies = data;
      });
  }

  closeHandler() {
    this.onSearchClose.emit();
  }

  handleStopPropagation(event: MouseEvent) {
    event.stopPropagation();
  }
}
