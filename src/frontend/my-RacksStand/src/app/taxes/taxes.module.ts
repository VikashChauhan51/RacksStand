import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { routedComponents, TaxesRoutingModule } from './taxes-routing.module';
import { FormsModule }   from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { TaxService } from './shared/tax.service';
import { LayoutModule } from '../layout/layout.module';
@NgModule({
  imports: [
      TaxesRoutingModule , CommonModule,
      FormsModule, SharedModule,LayoutModule
    ],
  declarations: [routedComponents],
  providers: [TaxService]
})
export class TaxesModule { }
