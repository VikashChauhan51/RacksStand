import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ResetPasswordRoutingModule, routedComponents } from './reset-password-routing.module';
import { EqualValidator } from '../../shared/validator/equal-validator.directive';
@NgModule({
    imports: [
        ResetPasswordRoutingModule, CommonModule,
        FormsModule
    ],
    declarations: [routedComponents, EqualValidator]
})
export class ResetPasswordModule { }
