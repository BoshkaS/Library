import { Component, OnInit } from '@angular/core';
import { RandomBookService } from './random.service';
import { FiltersService } from '../../catalog/filters/filter-item/filters.service';
import { BookDTO } from '../../dto/bookDTO.model';

@Component({
  selector: 'app-random-book',
  templateUrl: './random-book.component.html',
  styleUrl: './random-book.component.css',
})
export class RandomBookComponent implements OnInit {
  randomBook: BookDTO | null = null;
  categories: string[] = [];

  selectedOptions: string[] = [];

  constructor(
    private randomService: RandomBookService,
    private filterService: FiltersService
  ) {}

  ngOnInit(): void {
    this.randomService.getRandomBook().subscribe((bookDTO) => {
      this.randomBook = bookDTO;
      console.log(this.randomBook);
    });
    this.categories = this.filterService.dummyFilters.filter(
      (filter) => filter.name === 'Категорії'
    )[0].options;
  }

  addSelectedOptionHandler(attributes: { value: string }) {
    if (!this.selectedOptions.includes(attributes.value)) {
      this.selectedOptions = [...this.selectedOptions, attributes.value];
    } else {
      this.selectedOptions = this.selectedOptions.filter(
        (option) => option != attributes.value
      );
    }
  }

  generateRandomBook() {
    this.randomService.getRandomBook().subscribe((bookDTO) => {
      this.randomBook = bookDTO;
    });
  }
}
