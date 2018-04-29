import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddressModule } from './address/address.module';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
@NgModule({
  imports: [
    CommonModule, FormsModule, RouterModule,AddressModule
  ],
  exports: [
    CommonModule, FormsModule, RouterModule,AddressModule] ,
  declarations: []
})
export class AppCommonModule { }
