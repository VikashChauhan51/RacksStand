import { NgModule } from '@angular/core';
import { PreloadAllModules, Routes, RouterModule } from '@angular/router';
import { AuthGuardService } from './core/auth-guard.service';
import { CanDeactivateGuardService } from './core/can-deactivate-guard.service';
import { SessionService } from './core/session.service';
import { PageNotFoundComponent } from './shared/error-pages/page-not-found.component';

/***************************************************************
* Lazy Loading to Eager Loading
*
* 1. Remove the module and NgModule imports in `app.module.ts`
*
* 2. Remove the lazy load route from `app.routing.ts`
*
* 3. Change the module's default route path from '' to 'pathname'
*****************************************************************/
const appRoutes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'dashboard', },
  { path: 'dashboard', loadChildren: 'app/dashboard/dashboard.module#DashboardModule' },
  { path: '**', pathMatch: 'full', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes, { preloadingStrategy: PreloadAllModules })],
  exports: [RouterModule],
  providers: [AuthGuardService,
    CanDeactivateGuardService,
    SessionService ]
})
export class AppRoutingModule { }


 