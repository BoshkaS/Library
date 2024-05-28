import { Component, Input } from '@angular/core';
import { BookDTO } from '../../../dto/bookDTO.model';

@Component({
  selector: 'app-popular-book',
  templateUrl: './popular-book.component.html',
  styleUrl: './popular-book.component.css'
})
export class PopularBookComponent {
  @Input() popularBook: BookDTO;
}
