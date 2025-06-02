import { Component, Input, OnInit } from '@angular/core';
import { Book } from '../../../dto/models/book.model';
import { BookDTO } from '../../../dto/bookDTO.model';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrl: './book.component.css',
})
export class BookComponent implements OnInit {
  @Input() bookDTO: BookDTO;

  ngOnInit(): void {
    console.log(this.bookDTO);
  }
}
