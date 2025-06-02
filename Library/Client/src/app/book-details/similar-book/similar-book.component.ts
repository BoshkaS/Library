import { Component, Input } from '@angular/core';
import { BookDTO } from '../../dto/bookDTO.model';

@Component({
  selector: 'app-similar-book',
  templateUrl: './similar-book.component.html',
  styleUrl: './similar-book.component.css',
})
export class SimilarBookComponent {
  @Input() similarBook: BookDTO;
}
