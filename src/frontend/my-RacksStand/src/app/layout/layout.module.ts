
import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
//other components
import { NavComponent } from './nav/nav.component';
import { FooterComponent } from './footer/footer.component';
import { SidebarComponent } from './sidebar/sidebar.component';

@NgModule({
  imports: [
      CommonModule, FormsModule, RouterModule
  ]
  ,
  exports: [
      CommonModule, FormsModule, RouterModule, [NavComponent], [FooterComponent], [SidebarComponent]],
  declarations: [NavComponent, FooterComponent, SidebarComponent]
})
export class LayoutModule { 
   
}
