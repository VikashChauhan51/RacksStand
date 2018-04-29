import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { routedComponents, RacksRoutingModule } from './racks-routing.module';
import { FormsModule }   from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { RackService } from './shared/rack.service';
import { LayoutModule } from '../layout/layout.module';
@NgModule({
  imports: [
    RacksRoutingModule , CommonModule,
      FormsModule, SharedModule,LayoutModule
    ],
  declarations: [routedComponents],
  providers: [RackService]
})
export class RacksModule { }
