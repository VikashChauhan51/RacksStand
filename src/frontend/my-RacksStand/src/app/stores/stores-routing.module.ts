import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StoreComponent } from './store/store.component';
import { StoreListComponent } from './store-list/store-list.component';
import { StoresComponent } from './stores.component';
import { CanDeactivateGuardService } from '../core/can-deactivate-guard.service';
const routes: Routes = [
    {
        path: '',
        component: StoresComponent,
        children: [
            {
                path: '',
                component: StoreListComponent, data: { title: 'stores' },
            }
        ]
    },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class StoresRoutingModule { }
export const routedComponents = [StoreComponent, StoreListComponent, StoresComponent];