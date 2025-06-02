import { Location } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-create-author',
  templateUrl: './create-author.component.html',
  styleUrl: './create-author.component.css',
})
export class CreateAuthorComponent {
  @Output() onClose = new EventEmitter();

  constructor(private http: HttpClient, private toastr: ToastrService) {}

  onCreateAuthor(author: { pseudonym: string }) {
    this.http.post('https://localhost:5001/api/author', author).subscribe({
      next: (responseData) => this.toastr.success('Ви успішно додали автора!'),
      error: (error) => console.log(error),
      complete: () => console.log('Request has completed'),
    });
  }

  closeHandler() {
    this.onClose.emit();
  }

  handleStopPropagation(event: MouseEvent) {
    event.stopPropagation();
  }
}
