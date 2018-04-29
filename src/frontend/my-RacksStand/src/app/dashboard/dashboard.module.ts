import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule }   from '@angular/forms';
import { DashboardRoutingModule,routedComponents } from './dashboard-routing.module';
import { SharedModule } from '../shared/shared.module';
import { LayoutModule } from '../layout/layout.module';
@NgModule({
    imports: [DashboardRoutingModule, CommonModule, FormsModule,SharedModule,LayoutModule],
  declarations: [ routedComponents]
})
export class DashboardModule { }
 
