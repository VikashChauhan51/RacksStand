import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RackBoxesComponent } from './rack-boxes.component';
import { RackBoxListComponent } from './rack-box-list/rack-box-list.component';
import { RackBoxComponent } from './rack-box/rack-box.component';
import { InventoryComponent } from './inventory/inventory.component';
import { CanDeactivateGuardService } from '../core/can-deactivate-guard.service';
const routes: Routes = [
    {
        path: '',
        component: RackBoxesComponent,
        children: [
            {
                path: '',
                component: RackBoxListComponent, data: { title: 'rack-box' },
            }
        ]
    },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class RackBoxesRoutingModule { }
export const routedComponents = [RackBoxComponent, RackBoxesComponent,RackBoxListComponent,InventoryComponent];