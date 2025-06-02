import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProcessReturnDateComponent } from './process-return-date.component';

describe('ProcessReturnDateComponent', () => {
  let component: ProcessReturnDateComponent;
  let fixture: ComponentFixture<ProcessReturnDateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProcessReturnDateComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ProcessReturnDateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
