import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ContinueDateService } from './continue-date.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-continue-date',
  templateUrl: './continue-date.component.html',
  styleUrl: './continue-date.component.css',
})
export class ContinueDateComponent {
  @Output() onSearchClose = new EventEmitter();
  @Input() id!: number;

  constructor(
    private continueDateService: ContinueDateService,
    private toastr: ToastrService
  ) {}

  continueDate() {
    this.continueDateService.continueDate(this.id).subscribe({
      next: (responseData) =>
        this.toastr.success('Ви успішно подали заявку на продовження дати'),
      error: () => console.error('Помилка оброблена в HttpInterceptor'), // ⚠️ Видаляємо toastr.error тут
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
