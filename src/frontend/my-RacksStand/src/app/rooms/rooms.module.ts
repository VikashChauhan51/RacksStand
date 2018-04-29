import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { routedComponents, RoomsRoutingModule } from './rooms-routing.module';
import { FormsModule }   from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { RoomService } from './shared/room.service';
import { LayoutModule } from '../layout/layout.module';
@NgModule({
  imports: [
      RoomsRoutingModule , CommonModule,
      FormsModule, SharedModule,LayoutModule
    ],
  declarations: [routedComponents],
  providers: [RoomService]
})
export class RoomsModule { }
