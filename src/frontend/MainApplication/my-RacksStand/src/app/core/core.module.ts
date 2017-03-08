
import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
//other components
import { NavComponent } from './nav/nav.component';

@NgModule({
  imports: [
    CommonModule, FormsModule, RouterModule
  ]
  ,
  exports: [
    CommonModule, FormsModule, RouterModule, [NavComponent]
  ],
  declarations: [NavComponent]
})
export class CoreModule { 
  constructor( @Optional() @SkipSelf() parentModule: CoreModule) {
    //todo:
  }
}
