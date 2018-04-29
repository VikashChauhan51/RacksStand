import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { routedComponents, CustomersRoutingModule } from './customers-routing.module';
import { FormsModule }   from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { CustomerService } from './shared/customer.service';
import { LayoutModule } from '../layout/layout.module';
import { AppCommonModule } from '../common/app-common.module';
@NgModule({
  imports: [
      CustomersRoutingModule , CommonModule,
      FormsModule, SharedModule,LayoutModule,AppCommonModule
    ],
  declarations: [routedComponents],
  providers: [CustomerService]
})
export class CustomersModule { }
