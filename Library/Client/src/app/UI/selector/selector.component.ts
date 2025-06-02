import {
  AfterContentInit,
  Component,
  ContentChild,
  ElementRef,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  Renderer2,
  SimpleChanges,
  ViewChild,
  ViewEncapsulation,
} from '@angular/core';

@Component({
  selector: 'app-selector',
  templateUrl: './selector.component.html',
  styleUrl: './selector.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class SelectorComponent implements AfterContentInit, OnChanges, OnInit {
  @Input() values: string[] = [];
  @Input() placeholder: string = '';
  @Input() active: string = '';
  @Input() extends: boolean = false;

  @ViewChild('dropdownDiv') dropDownDiv: ElementRef;

  @Output() onSelect = new EventEmitter<{ value: string }>();
  @ContentChild('selectorOptions') options: ElementRef<HTMLUListElement>;

  isContentInit: boolean = false;

  constructor(private renderer: Renderer2) {}

  ngOnInit(): void {}

  ngAfterContentInit(): void {
    this.isContentInit = true;
    const list = this.options.nativeElement;
    this.renderer.addClass(list, 'option-list');
    const options = Array.from(list.children);
    //console.log(options);
    options.forEach((option) => {
      this.renderer.addClass(option, 'option');
      option.addEventListener('click', () => {
        //console.log(option.getAttribute('optionValue'));
        this.onSelect.emit({ value: option.getAttribute('optionValue') });
      });
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.values && this.isContentInit) {
      const currentValue = changes.values.currentValue;
      const list = this.options.nativeElement;
      const options = Array.from(list.children);

      options.forEach((option) => {
        const optionValue = option.getAttribute('optionValue');
        const isCurrentValueIncludesOption = currentValue.includes(optionValue);
        if (isCurrentValueIncludesOption) {
          this.renderer.addClass(option, this.active);
        } else {
          this.renderer.removeClass(option, this.active);
        }
      });
    }
  }
}
