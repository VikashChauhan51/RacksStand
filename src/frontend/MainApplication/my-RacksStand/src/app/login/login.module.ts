import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginRoutingModule,routedComponents } from './login-routing.module';
import { SharedModule } from '../shared/shared.module';
@NgModule({
  imports: [
    CommonModule,
    SharedModule
  ],
  declarations: [routedComponents]
})
export class LoginModule { }
