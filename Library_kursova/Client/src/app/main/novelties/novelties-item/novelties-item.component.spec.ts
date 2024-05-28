import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NoveltiesItemComponent } from './novelties-item.component';

describe('NoveltiesItemComponent', () => {
  let component: NoveltiesItemComponent;
  let fixture: ComponentFixture<NoveltiesItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NoveltiesItemComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(NoveltiesItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
