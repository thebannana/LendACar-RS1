import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AdminLoginComponent} from './admin-login/admin-login.component';
import {AdminDashboardComponent} from './admin-dashboard/admin-dashboard.component';
import {AdminProfileOverviewComponent} from './admin-profile-overview/admin-profile-overview.component';
import {AdminEditOwnProfileComponent} from './admin-edit-own-profile/admin-edit-own-profile.component';
import { VehicleCategoriesComponent } from './vehicle-category/vehicle-category.component';

const routes: Routes = [
  {path:'',redirectTo:'login' ,pathMatch:'full'},
  {path:'login', component: AdminLoginComponent},
  {path:'dashboard', component: AdminDashboardComponent},
  {path:'profileOverview', component: AdminProfileOverviewComponent},
  {path:'profileEdit',component:AdminEditOwnProfileComponent},
  {path:'vehicleCategories',component: VehicleCategoriesComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdministratorRoutingModule { }