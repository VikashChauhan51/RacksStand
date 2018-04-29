import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { routedComponents, StoresRoutingModule } from './stores-routing.module';
import { FormsModule }   from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { StoreService } from './shared/store.service';
import { LayoutModule } from '../layout/layout.module';
@NgModule({
  imports: [
      StoresRoutingModule , CommonModule,
      FormsModule, SharedModule,LayoutModule
    ],
  declarations: [routedComponents],
  providers: [StoreService]
})
export class StoresModule { }
