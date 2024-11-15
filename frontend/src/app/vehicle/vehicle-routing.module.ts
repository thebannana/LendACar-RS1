import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LayoutComponent} from './layout/layout.component';
import {DashboardComponent} from './dashboard/dashboard.component';
import { VehicleCategoriesComponent } from './vehicle-category/vehicle-category.component';

const routes: Routes = [
  {
    path: '', component: LayoutComponent, children: [
      {path: '', redirectTo: 'dashboard', pathMatch: 'full'},
      {path:'dashboard', component:DashboardComponent},
      {path: '**', redirectTo: 'home', pathMatch: 'full'},
      {path:'vehicle-category',component: VehicleCategoriesComponent}
    ]
  }
];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VehicleRoutingModule { }
