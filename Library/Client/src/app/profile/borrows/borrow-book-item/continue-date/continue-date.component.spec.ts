import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContinueDateComponent } from './continue-date.component';

describe('ContinueDateComponent', () => {
  let component: ContinueDateComponent;
  let fixture: ComponentFixture<ContinueDateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ContinueDateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ContinueDateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
