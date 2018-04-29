import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { routedComponents, RackBoxesRoutingModule } from './rack-boxes-routing.module';
import { FormsModule }   from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { RackBoxService } from './shared/rack-box.service';
import { InventoryService } from './shared/inventory.service';
import { LayoutModule } from '../layout/layout.module';
@NgModule({
  imports: [
    RackBoxesRoutingModule , CommonModule,
      FormsModule, SharedModule,LayoutModule
    ],
  declarations: [routedComponents],
  providers: [RackBoxService,InventoryService]
})
export class RackBoxesModule { }
