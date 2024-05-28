import { Location } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Output } from '@angular/core';
import { PublishingHouse } from '../../dto/models/publising-house.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-create-publishing-house',
  templateUrl: './create-publishing-house.component.html',
  styleUrl: './create-publishing-house.component.css',
})
export class CreatePublishingHouseComponent {
  @Output() onClose = new EventEmitter();

  constructor(private http: HttpClient, private toastr: ToastrService) {}

  onCreateHouse(publishingHouse: PublishingHouse) {
    console.log(publishingHouse);
    this.http
      .post('https://localhost:5001/api/publishinghouse', publishingHouse)
      .subscribe({
        next: (responseData) =>
          this.toastr.success('Ви успішно додали видавництво!'),
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
