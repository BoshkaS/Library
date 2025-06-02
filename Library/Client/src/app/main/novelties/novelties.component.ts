import { Component, OnInit } from '@angular/core';
import { NoveltiesItemService } from './novelties.service';
import { Book } from '../../dto/models/book.model';

@Component({
  selector: 'app-novelties',
  templateUrl: './novelties.component.html',
  styleUrl: './novelties.component.css'
})
export class NoveltiesComponent implements OnInit{
  novelties: Book [] = [];
  
  constructor(private noveltiesItemService: NoveltiesItemService) {}

  ngOnInit () {
    this.noveltiesItemService.getNovelties().subscribe(books => {
      this.novelties = books;
    })
  }
}
