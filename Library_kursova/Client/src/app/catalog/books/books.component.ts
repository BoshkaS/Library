import { Component, OnInit } from '@angular/core';
import { BooksService } from './books.service';
import { BookDTO } from '../../dto/bookDTO.model';
import { Pagination } from '../../dto/models/pagination';
import { BookParams } from '../../dto/models/bookParams';
import { FilterDTO } from '../../dto/filterDTo.models';
import { FiltersService } from '../filters/filter-item/filters.service';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrl: './books.component.css',
})
export class BooksComponent implements OnInit {
  selectedFilter: string = 'like';

  books: BookDTO[] = [];
  pagination: Pagination | undefined;
  bookParams: BookParams | undefined;

  filterDTO: FilterDTO = {
    categories: [],
    publishingHouses: [],
    languages: [],
  };

  constructor(
    private booksService: BooksService,
    private filtersService: FiltersService
  ) {
    this.bookParams = new BookParams(this.selectedFilter);
  }

  ngOnInit(): void {
    this.filtersService.getFilterDTO().subscribe((filterDTO) => {
      this.filterDTO = filterDTO;
      this.loadBooks(); // Call your method to load books with the new filter
    });

    console.log(this.filtersService.dummyFilters[0].options);
  }

  onFilterChange() {
    this.bookParams = new BookParams(this.selectedFilter);
    this.loadBooks();
  }

  loadBooks() {
    this.booksService.filterBooks(this.bookParams, this.filterDTO).subscribe({
      next: (response) => {
        if (response.result && response.pagination) {
          this.books = response.result;

          this.pagination = response.pagination;
        }
      },
    });
  }

  pageChanged(event: any) {
    if (this.bookParams?.pageNumber !== event.page) {
      this.bookParams.pageNumber = event.page;
      console.log(this.pagination);
      console.log(this.bookParams);
      this.loadBooks();
    }
  }
}
