import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ForgotPasswordRoutingModule, routedComponents } from './forgot-password-routing.module';
import { FormsModule }   from '@angular/forms';
@NgModule({
  imports: [
      ForgotPasswordRoutingModule, CommonModule,
      FormsModule
      
  ],
  declarations: [routedComponents]
})
export class ForgotPasswordModule { }
