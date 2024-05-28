import { Component, Input, OnInit } from '@angular/core';
import { BookDTO } from '../../dto/bookDTO.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-search-book-item',
  templateUrl: './search-book-item.component.html',
  styleUrl: './search-book-item.component.css',
})
export class SearchBookItemComponent implements OnInit {
  @Input() searchBook: BookDTO;

  constructor(private router: Router) {}

  ngOnInit(): void {
    console.log(this.searchBook);
  }

  
}
