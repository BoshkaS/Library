import { Component, EventEmitter, Input, OnInit, Output, afterRender } from '@angular/core';
import { FiltersService } from './filters.service';

@Component({
  selector: 'app-filter-item',
  templateUrl: './filter-item.component.html',
  styleUrl: './filter-item.component.css'
})
export class FilterItemComponent implements OnInit {
  isOpen: boolean = false;

  constructor(private filtersService: FiltersService) {}

  ngOnInit(): void {
    
  }

  @Input() selectedOptions: string[] = []; 
  @Input() placeholder: string = '';
  @Input() active: string = '';
  @Input() extends: boolean = false;
  @Input() options: string[] = []

  openHandler() {
    this.isOpen = !this.isOpen;
  }

  selectHandler(attributes: {value: string}){
    this.filtersService.selectOption(this.placeholder, attributes.value);
  }
}
