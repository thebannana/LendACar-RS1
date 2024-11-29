import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdministratorRoutingModule } from './administrator-routing.module';
import { AdminLoginComponent } from './admin-login/admin-login.component';
import { AdminProfileOverviewComponent } from './admin-profile-overview/admin-profile-overview.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { AdminEditOwnProfileComponent } from './admin-edit-own-profile/admin-edit-own-profile.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { VehicleCategoriesComponent } from './vehicle-category/vehicle-category.component';
import { AdminEmployeeOverviewComponent } from './admin-employee-overview/admin-employee-overview.component';
import { VehicleOverviewComponent } from './vehicle-overview/vehicle-overview.component';
import { UserOverviewComponent } from './user-overview/user-overview.component';



@NgModule({
  declarations: [
    AdminLoginComponent,
    AdminProfileOverviewComponent,
    AdminDashboardComponent,
    AdminEditOwnProfileComponent,
    VehicleCategoriesComponent,
    AdminEmployeeOverviewComponent,
    VehicleOverviewComponent,
    UserOverviewComponent
  ],
  imports: [
    CommonModule,
    AdministratorRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class AdministratorModule { }
