import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BorrowsService } from '../../borrows.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-return-book',
  templateUrl: './return-book.component.html',
  styleUrl: './return-book.component.css',
})
export class ReturnBookComponent {
  @Output() onSearchClose = new EventEmitter();
  @Input() id!: number;

  condition: string = 'Good'; // за замовчуванням

  conditionDropdownOpen = false;

  conditionOptions = [
    { value: 'Good', label: 'Гарний' },
    { value: 'Damaged', label: 'Пошкоджений' },
  ];

  constructor(
    private borrowsService: BorrowsService,
    private toastr: ToastrService
  ) {}

  get selectedConditionLabel(): string {
    return (
      this.conditionOptions.find((opt) => opt.value === this.condition)
        ?.label || 'Оберіть стан книги'
    );
  }

  toggleConditionDropdown() {
    this.conditionDropdownOpen = !this.conditionDropdownOpen;
  }

  selectCondition(value: string, event: MouseEvent) {
    event.stopPropagation();
    this.condition = value;
    this.conditionDropdownOpen = false;
  }

  returnBook() {
    this.borrowsService.returnBook(this.id, this.condition).subscribe({
      next: () => this.toastr.success('Ви успішно здійснили повернення книги!'),
      error: () => console.error('Помилка оброблена в HttpInterceptor'),
      complete: () => console.log('Request has completed'),
    });
  }

  closeHandler() {
    this.onSearchClose.emit();
  }

  handleStopPropagation(event: MouseEvent) {
    event.stopPropagation();
  }
}
