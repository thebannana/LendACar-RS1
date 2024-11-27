import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminEditOwnProfileComponent } from './admin-edit-own-profile.component';

describe('AdminEditOwnProfileComponent', () => {
  let component: AdminEditOwnProfileComponent;
  let fixture: ComponentFixture<AdminEditOwnProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdminEditOwnProfileComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminEditOwnProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
