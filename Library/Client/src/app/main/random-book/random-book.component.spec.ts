import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RandomBookComponent } from './random-book.component';

describe('RandomBookComponent', () => {
  let component: RandomBookComponent;
  let fixture: ComponentFixture<RandomBookComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RandomBookComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RandomBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
