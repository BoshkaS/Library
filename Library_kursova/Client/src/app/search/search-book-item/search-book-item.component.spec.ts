import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchBookItemComponent } from './search-book-item.component';

describe('SearchBookItemComponent', () => {
  let component: SearchBookItemComponent;
  let fixture: ComponentFixture<SearchBookItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SearchBookItemComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SearchBookItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
