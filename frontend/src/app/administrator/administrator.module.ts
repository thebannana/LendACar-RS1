import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdministratorRoutingModule } from './administrator-routing.module';
import { AdminLoginComponent } from './admin-login/admin-login.component';
import { AdminProfileOverviewComponent } from './admin-profile-overview/admin-profile-overview.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { AdminEditOwnProfileComponent } from './admin-edit-own-profile/admin-edit-own-profile.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';



@NgModule({
  declarations: [
    AdminLoginComponent,
    AdminProfileOverviewComponent,
    AdminDashboardComponent,
    AdminEditOwnProfileComponent
  ],
  imports: [
    CommonModule,
    AdministratorRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class AdministratorModule { }
