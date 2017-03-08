import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { InitCapsPipe } from './shared/string-pipe/init-caps.pipe';
import { PageNotFoundComponent } from './shared/error-pages/page-not-found.component';
import { AppRoutingModule } from './app-routing.module';
import './core/rxjs-extensions';
/* Feature Modules */
import { CoreModule } from './core/core.module';
import { LoginModule } from './login/login.module';
  
@NgModule({
  imports: [BrowserModule,FormsModule,HttpModule,
   LoginModule,
   // Routes get loaded in order. It is important that login
    // come before AppRoutingModule, as
    // AppRoutingModule defines the catch-all ** route
    AppRoutingModule,
    CoreModule,],
  declarations: [AppComponent,PageNotFoundComponent],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}