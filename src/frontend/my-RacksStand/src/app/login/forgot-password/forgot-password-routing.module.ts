import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ForgotPasswordComponent } from './forgot-password.component';

const routes: Routes = [
    { path: '', component: ForgotPasswordComponent, data: { title: 'Forgot Password'} },
];
export const routedComponents = [ForgotPasswordComponent];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class ForgotPasswordRoutingModule { }