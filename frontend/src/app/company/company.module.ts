import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CompanyRoutingModule } from './company-routing.module';
import {LayoutComponent} from './layout/layout.component';
import {DashboardComponent} from './dashboard/dashboard.component';
import {FormsModule} from "@angular/forms";


@NgModule({
  declarations: [
    LayoutComponent,
    DashboardComponent
  ],
    imports: [
        CommonModule,
        CompanyRoutingModule,
        FormsModule
    ]
})
export class CompanyModule { }
