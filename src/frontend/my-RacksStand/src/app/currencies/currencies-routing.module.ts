import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CurrencyComponent } from './currency/currency.component';
import { CurrencyListComponent } from './currency-list/currency-list.component';
import { CurrenciesComponent } from './currencies.component';
import { CanDeactivateGuardService } from '../core/can-deactivate-guard.service';
const routes: Routes = [
    {
        path: '',
        component: CurrenciesComponent,
        children: [
            {
                path: '',
                component: CurrencyListComponent, data: { title: 'currencies' },
            }
        ]
    },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class CurrenciesRoutingModule { }
export const routedComponents = [CurrencyComponent, CurrencyListComponent, CurrenciesComponent];