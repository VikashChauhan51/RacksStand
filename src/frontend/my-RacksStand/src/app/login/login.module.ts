import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule }   from '@angular/forms';

import { LoginRoutingModule,routedComponents } from './login-routing.module';


@NgModule({
  imports: [
      LoginRoutingModule, CommonModule,
      FormsModule 
  ],
  declarations: [routedComponents]
})
export class LoginModule { }
