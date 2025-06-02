import { Component, OnInit } from '@angular/core';
import { PopularBooksService } from './popular.service';
import { BookDTO } from '../../dto/bookDTO.model';

@Component({
  selector: 'app-popular-books',
  templateUrl: './popular-books.component.html',
  styleUrl: './popular-books.component.css'
})
export class PopularBooksComponent implements OnInit{
  popularBooks : BookDTO [] = [];

  constructor(private popularBooksService: PopularBooksService){}

  ngOnInit(): void {
    this.popularBooksService.getPopularBook().subscribe(booksDTO => {
      this.popularBooks = booksDTO;
    })
  }
}
