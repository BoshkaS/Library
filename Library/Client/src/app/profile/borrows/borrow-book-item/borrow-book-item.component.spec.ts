import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BorrowBookItemComponent } from './borrow-book-item.component';

describe('BorrowBookItemComponent', () => {
  let component: BorrowBookItemComponent;
  let fixture: ComponentFixture<BorrowBookItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BorrowBookItemComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BorrowBookItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
