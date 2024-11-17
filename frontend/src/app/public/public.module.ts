import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PublicRoutingModule } from './public-routing.module';
import {HomeComponent} from './home/home.component';
import {LayoutComponent} from './layout/layout.component';
import {DataRowOutlet} from "@angular/cdk/table";


@NgModule({
  declarations: [
    HomeComponent,
    LayoutComponent
  ],
    imports: [
        CommonModule,
        PublicRoutingModule,
        DataRowOutlet
    ]
})
export class PublicModule { }
