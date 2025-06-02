import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimilarBookComponent } from './similar-book.component';

describe('SimilarBookComponent', () => {
  let component: SimilarBookComponent;
  let fixture: ComponentFixture<SimilarBookComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SimilarBookComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SimilarBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
