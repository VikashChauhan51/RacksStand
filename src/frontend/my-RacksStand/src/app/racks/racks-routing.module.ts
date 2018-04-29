import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RackComponent } from './rack/rack.component';
import { RackListComponent } from './rack-list/rack-list.component';
import { RacksComponent } from './racks.component';
import { CanDeactivateGuardService } from '../core/can-deactivate-guard.service';
const routes: Routes = [
    {
        path: '',
        component: RacksComponent,
        children: [
            {
                path: '',
                component: RackListComponent, data: { title: 'racks' },
            }
        ]
    },
    
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class RacksRoutingModule { }
export const routedComponents = [RackComponent, RackListComponent, RacksComponent];