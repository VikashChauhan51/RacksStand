import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SupplierComponent } from './supplier/supplier.component';
import { SupplierListComponent } from './supplier-list/supplier-list.component';
import { SuppliersComponent } from './suppliers.component';
import { CanDeactivateGuardService } from '../core/can-deactivate-guard.service';
import { SupplierAddressComponent } from './supplier-address/supplier-address.component';
const routes: Routes = [
    {
        path: '',
        component: SuppliersComponent,
        children: [
            {
                path: '',
                component: SupplierListComponent, data: { title: 'suppliers' },
            },
            {
                path: ':id',
                component: SupplierComponent, data: { title: 'suppliers' }
            },
        ]
    },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class SuppliersRoutingModule { }
export const routedComponents = [SupplierComponent, SupplierListComponent, SuppliersComponent,SupplierAddressComponent];