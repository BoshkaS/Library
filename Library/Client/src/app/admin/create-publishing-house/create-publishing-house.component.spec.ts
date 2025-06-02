import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatePublishingHouseComponent } from './create-publishing-house.component';

describe('CreatePublishingHouseComponent', () => {
  let component: CreatePublishingHouseComponent;
  let fixture: ComponentFixture<CreatePublishingHouseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreatePublishingHouseComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreatePublishingHouseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
