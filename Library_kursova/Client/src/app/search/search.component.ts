import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { SearchBookService } from './search.service';
import { BookDTO } from '../dto/bookDTO.model';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrl: './search.component.css',
})
export class SearchComponent {
  @Output() onSearchClose = new EventEmitter();
  searchString: string = '';

  booksDTO: BookDTO[] = [];

  constructor(private searchService: SearchBookService) {}

  closeHandler() {
    this.onSearchClose.emit();
  }

  handleStopPropagation(event: MouseEvent) {
    event.stopPropagation();
  }
  

  onSearch() {
    this.searchService
      .getSearchBook(this.searchString)
      .subscribe((booksDTO) => {
        this.booksDTO = booksDTO;
        //console.log(this.booksDTO);
      });
  }
}
