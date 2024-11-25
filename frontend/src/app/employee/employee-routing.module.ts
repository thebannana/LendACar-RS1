import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from './login/login.component';
import {DashboardComponent} from './dashboard/dashboard.component';
import {ProfileOverviewComponent} from './profile-overview/profile-overview.component';
import {EditProfileComponent} from './edit-profile/edit-profile.component';

const routes: Routes = [
  {path:'',redirectTo:'login' ,pathMatch:'full'},
  {path:'login', component: LoginComponent},
  {path:'dashboard', component: DashboardComponent},
  {path:'profileOverview', component: ProfileOverviewComponent},
  {path:'profileEdit',component:EditProfileComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule { }
