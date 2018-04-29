import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { routedComponents, CurrenciesRoutingModule } from './currencies-routing.module';
import { FormsModule }   from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { CurrencyService } from './shared/currency.service';
import { LayoutModule } from '../layout/layout.module';
@NgModule({
  imports: [
      CurrenciesRoutingModule , CommonModule,
      FormsModule, SharedModule,LayoutModule
    ],
  declarations: [routedComponents],
  providers: [CurrencyService]
})
export class CurrenciesModule { }
