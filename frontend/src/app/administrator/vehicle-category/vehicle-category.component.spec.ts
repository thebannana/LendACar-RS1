import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VehicleCategoriesComponent } from './vehicle-category.component';

describe('VehicleCategoryComponent', () => {
  let component: VehicleCategoriesComponent;
  let fixture: ComponentFixture<VehicleCategoriesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [VehicleCategoriesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VehicleCategoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
