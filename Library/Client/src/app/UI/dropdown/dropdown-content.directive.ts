import { Directive, ElementRef, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appDropdownContent]',
})
export class DropdownContentDirective {
  constructor(private elementRef: ElementRef, private renderer: Renderer2) {}

  ngOnInit(): void {
    this.renderer.addClass(this.elementRef.nativeElement, 'dropdown-content');
  }
}
