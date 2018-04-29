import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TaxComponent } from './tax/tax.component';
import { TaxListComponent } from './tax-list/tax-list.component';
import { TaxesComponent } from './taxes.component';
import { CanDeactivateGuardService } from '../core/can-deactivate-guard.service';
const routes: Routes = [
    {
        path: '',
        component: TaxesComponent,
        children: [
            {
                path: '',
                component: TaxListComponent, data: { title: 'taxes' },
            }
        ]
    },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class TaxesRoutingModule { }
export const routedComponents = [TaxComponent, TaxListComponent, TaxesComponent];