import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { routedComponents, SuppliersRoutingModule } from './suppliers-routing.module';
import { FormsModule }   from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { SupplierService } from './shared/supplier.service';
import { LayoutModule } from '../layout/layout.module';
import { AppCommonModule } from '../common/app-common.module';
@NgModule({
  imports: [
      SuppliersRoutingModule , CommonModule,
      FormsModule, SharedModule,LayoutModule,AppCommonModule
    ],
  declarations: [routedComponents],
  providers: [SupplierService]
})
export class SuppliersModule { }
