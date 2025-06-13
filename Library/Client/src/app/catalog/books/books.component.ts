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
  dropdownOpen = false;
  isLoading = true;

  books: BookDTO[] = [];
  pagination: Pagination | undefined;
  bookParams: BookParams | undefined;

  filters = [
    { value: 'like', label: 'За кількістю вподобайок' },
    { value: 'new', label: 'За новизною' },
  ];

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
  }

  get selectedFilterLabel(): string {
    return (
      this.filters.find((f) => f.value === this.selectedFilter)?.label ||
      'Оберіть фільтр'
    );
  }

  toggleDropdown() {
    this.dropdownOpen = !this.dropdownOpen;
  }

  selectFilter(value: string) {
    this.selectedFilter = value;
    this.dropdownOpen = false;
    this.onFilterChange();
  }

  onFilterChange() {
    this.bookParams = new BookParams(this.selectedFilter);
    this.loadBooks();
  }

  loadBooks() {
    this.isLoading = true;
    setTimeout(() => {
      this.booksService.filterBooks(this.bookParams, this.filterDTO).subscribe({
        next: (response) => {
          if (response.result && response.pagination) {
            this.books = response.result;
            this.isLoading = false;
            this.pagination = response.pagination;
          }
        },
      });
    }, 700);
  }

  pageChanged(event: any) {
    if (this.bookParams?.pageNumber !== event.page) {
      this.bookParams.pageNumber = event.page;
      this.loadBooks();
    }
  }
}
