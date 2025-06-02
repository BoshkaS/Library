import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserRatingLogComponent } from './user-rating-log.component';

describe('UserRatingLogComponent', () => {
  let component: UserRatingLogComponent;
  let fixture: ComponentFixture<UserRatingLogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UserRatingLogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserRatingLogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
