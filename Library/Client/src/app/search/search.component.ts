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
  isLoading: boolean = false;
  hasSearched: boolean = false;

  booksDTO: BookDTO[] = [];

  constructor(private searchService: SearchBookService) {}

  closeHandler() {
    this.onSearchClose.emit();
  }

  handleStopPropagation(event: MouseEvent) {
    event.stopPropagation();
  }

  onSearch() {
    if (this.searchString.length < 3) return;

    this.isLoading = true;
    this.hasSearched = true;
    this.booksDTO = [];

    this.searchService.getSearchBook(this.searchString).subscribe({
      next: (booksDTO) => {
        this.booksDTO = booksDTO;
        this.isLoading = false;
      },
      error: () => {
        this.booksDTO = [];
        this.isLoading = false;
      },
    });
  }
}
