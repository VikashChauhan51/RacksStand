import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RoomComponent } from './room/room.component';
import { RoomListComponent } from './room-list/room-list.component';
import { RoomsComponent } from './rooms.component';
import { CanDeactivateGuardService } from '../core/can-deactivate-guard.service';
const routes: Routes = [
    {
        path: '',
        component: RoomsComponent,
        children: [
            {
                path: '',
                component: RoomListComponent, data: { title: 'rooms' },
            }
        ]
    },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class RoomsRoutingModule { }
export const routedComponents = [RoomComponent, RoomListComponent, RoomsComponent];