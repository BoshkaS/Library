import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { Category } from '../../../dto/models/category.model';
import { Injectable } from '@angular/core';
import { PublishingHouse } from '../../../dto/models/publising-house.model';
import { Language } from '../../../dto/models/language.model';
import { FilterDTO } from '../../../dto/filterDTo.models';

export type Filter = {
  name: string;
  options: string[];
  selectedOptions: string[];
};
@Injectable()
export class FiltersService {
  categoriesNames: string[] = [];
  private filterDTOSubject: BehaviorSubject<FilterDTO> =
    new BehaviorSubject<FilterDTO>({
      categories: [],
      publishingHouses: [],
      languages: [],
    });

  constructor(private http: HttpClient) {}

  dummyFilters: Filter[] = [
    { name: 'Категорії', options: [], selectedOptions: [] },
    { name: 'Видавництва', options: [], selectedOptions: [] },
    { name: 'Мова', options: [], selectedOptions: [] },
  ];

  setFilterDTO(filterDTO: FilterDTO) {
    this.filterDTOSubject.next(filterDTO);
  }

  getFilterDTO(): Observable<FilterDTO> {
    return this.filterDTOSubject.asObservable();
  }

  public getPublishingHouse() {
    return this.http
      .get<PublishingHouse[]>('https://localhost:5001/api/publishinghouse')
      .pipe(
        map((responseData: PublishingHouse[]) => {
          const publishingHouseNames = responseData.map((item) => item.name);
          this.dummyFilters[1].options = publishingHouseNames;
          return publishingHouseNames;
        })
      );
  }

  public getCategories() {
    return this.http
      .get<Category[]>('https://localhost:5001/api/category')
      .pipe(
        map((responseData: Category[]) => {
          const categoryNames = responseData.map((item) => item.name);
          this.dummyFilters[0].options = categoryNames;
          return categoryNames;
        })
      );
  }

  public getLanguage() {
    return this.http
      .get<Language[]>('https://localhost:5001/api/language')
      .pipe(
        map((responseData: Language[]) => {
          const languageNames = responseData.map((item) => item.name);
          this.dummyFilters[2].options = languageNames;
          console.log('Languages Response:', languageNames);
          return languageNames;
        })
      );
  }

  selectOption(filterName: string, option: string) {
    const currentFilter = this.dummyFilters.filter(
      (filter) => filter.name === filterName
    )[0];
    if (!currentFilter.selectedOptions.includes(option)) {
      currentFilter.selectedOptions = [
        ...currentFilter.selectedOptions,
        option,
      ];
    } else {
      currentFilter.selectedOptions = currentFilter.selectedOptions.filter(
        (selectedOption) => selectedOption != option
      );
    }
  }

  cleanFilters() {
    this.dummyFilters.forEach(function (value) {
      value.selectedOptions = [];
    });
  }
}
