import { Component, OnInit } from '@angular/core';
import { Filter, FiltersService } from './filter-item/filters.service';
import { FilterDTO } from '../../dto/filterDTo.models';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-filters',
  templateUrl: './filters.component.html',
  styleUrl: './filters.component.css',
})
export class FiltersComponent implements OnInit {
  isOpen: boolean = false;
  selectedOptions: string[] = [];
  filterDTO: FilterDTO | undefined;
  dummyFilters: Filter[] = [];
  isLoading: boolean = true;

  openHandler() {
    this.isOpen = !this.isOpen;
  }

  constructor(private filtersService: FiltersService) {}

  ngOnInit(): void {
    forkJoin([
      this.filtersService.getCategories(),
      this.filtersService.getPublishingHouse(),
      this.filtersService.getLanguage(),
    ]).subscribe(([categoryNames, publishingHouseNames, languageNames]) => {
      this.dummyFilters[0].options = categoryNames;
      this.dummyFilters[1].options = publishingHouseNames;
      this.dummyFilters[2].options = languageNames;
      this.isLoading = false; // Set loading state to false when all data is fetched
    });

    this.dummyFilters = this.filtersService.dummyFilters;
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

  cleanFilters() {
    this.filtersService.cleanFilters();
  }

  addFilters() {
    this.filterDTO = {
      categories: this.dummyFilters[0].selectedOptions,
      publishingHouses: this.dummyFilters[1].selectedOptions,
      languages: this.dummyFilters[2].selectedOptions,
    };
    this.filtersService.setFilterDTO(this.filterDTO);
  }
}
