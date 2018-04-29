import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CustomerComponent } from './customer/customer.component';
import { CustomerListComponent } from './customer-list/customer-list.component';
import { CustomersComponent } from './customers.component';
import { CustomerAddressComponent } from './customer-address/customer-address.component';
import { CanDeactivateGuardService } from '../core/can-deactivate-guard.service';
const routes: Routes = [
    {
        path: '',
        component: CustomersComponent,
        children: [
            {
                path: '',
                component: CustomerListComponent, data: { title: 'customers' },
            },
            {
                path: ':id',
                component: CustomerComponent, data: { title: 'customers' }
            },
        ]
    },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class CustomersRoutingModule { }
export const routedComponents = [CustomerComponent, CustomerListComponent, CustomersComponent,CustomerAddressComponent];