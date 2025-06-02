import { Component, Input } from '@angular/core';
import { Book } from '../../../dto/models/book.model';

@Component({
  selector: 'app-novelties-item',
  templateUrl: './novelties-item.component.html',
  styleUrl: './novelties-item.component.css',
})
export class NoveltiesItemComponent {
  @Input() noveltie: Book;
}
