
import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { throwIfAlreadyLoaded } from './module-import-guard';
//error pages
import { PageNotFoundComponent } from './error-pages/page-not-found/page-not-found.component';
import { ServerErrorComponent } from './error-pages/server-error/server-error.component';
//other modules
import { ToastModule } from './toast/toast.module';
import { SpinnerModule } from './spinner/spinner.module';
import { ModalModule } from './modal/modal.module';

@NgModule({
  imports: [
      CommonModule, FormsModule, RouterModule, ToastModule, SpinnerModule, ModalModule
  ]
  ,
  exports: [
      CommonModule, FormsModule, RouterModule, PageNotFoundComponent, [ToastModule], [SpinnerModule],
      [ModalModule]],
  declarations: [ PageNotFoundComponent, ServerErrorComponent]
})
export class CoreModule { 
  constructor( @Optional() @SkipSelf() parentModule: CoreModule) {
      throwIfAlreadyLoaded(parentModule, 'CoreModule');
  }
}
