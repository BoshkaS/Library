import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateBorrowingComponent } from './create-borrowing.component';

describe('CreateBorrowingComponent', () => {
  let component: CreateBorrowingComponent;
  let fixture: ComponentFixture<CreateBorrowingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateBorrowingComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateBorrowingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
