import { NgModule } from '@angular/core';
import { PreloadAllModules, Routes, RouterModule } from '@angular/router';
import { AuthGuardService } from './core/auth-guard.service';
import { CanDeactivateGuardService } from './core/can-deactivate-guard.service';
import { SessionService } from './core/session.service';
import { CookieService } from './core/cookie.service';
//error compoment
import { PageNotFoundComponent } from './core/error-pages/page-not-found/page-not-found.component';
import { ServerErrorComponent } from './core/error-pages/server-error/server-error.component';

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
    {
        path: 'dashboard', loadChildren: 'app/dashboard/dashboard.module#DashboardModule', canActivate: [AuthGuardService],
        canActivateChild: [AuthGuardService],
        canLoad: [AuthGuardService]
    },
    { path: 'login', loadChildren: 'app/login/login.module#LoginModule' },
    { path: 'forgotPassword', loadChildren: 'app/login/forgot-password/forgot-password.module#ForgotPasswordModule' },
    { path: 'resetPassword', loadChildren: 'app/login/reset-password/reset-password.module#ResetPasswordModule' },
    {
        path: 'customers', loadChildren: 'app/customers/customers.module#CustomersModule', canActivate: [AuthGuardService],
        canActivateChild: [AuthGuardService],
        canLoad: [AuthGuardService]
    },
    {
        path: 'suppliers', loadChildren: 'app/suppliers/suppliers.module#SuppliersModule', canActivate: [AuthGuardService],
        canActivateChild: [AuthGuardService],
        canLoad: [AuthGuardService]
    },
    {
        path: 'stores', loadChildren: 'app/stores/stores.module#StoresModule', canActivate: [AuthGuardService],
        canActivateChild: [AuthGuardService],
        canLoad: [AuthGuardService]
    },
    {
        path: 'rooms', loadChildren: 'app/rooms/rooms.module#RoomsModule', canActivate: [AuthGuardService],
        canActivateChild: [AuthGuardService],
        canLoad: [AuthGuardService]
    },
    {
        path: 'racks', loadChildren: 'app/racks/racks.module#RacksModule', canActivate: [AuthGuardService],
        canActivateChild: [AuthGuardService],
        canLoad: [AuthGuardService]
    },
    {
        path: 'rack-boxes', loadChildren: 'app/rack-boxes/rack-boxes.module#RackBoxesModule', canActivate: [AuthGuardService],
        canActivateChild: [AuthGuardService],
        canLoad: [AuthGuardService]
    },
    {
        path: 'currencies', loadChildren: 'app/currencies/currencies.module#CurrenciesModule', canActivate: [AuthGuardService],
        canActivateChild: [AuthGuardService],
        canLoad: [AuthGuardService]
    },
    {
        path: 'taxes', loadChildren: 'app/taxes/taxes.module#TaxesModule', canActivate: [AuthGuardService],
        canActivateChild: [AuthGuardService],
        canLoad: [AuthGuardService]
    },
    { path: 'error', component: ServerErrorComponent },
    { path: '**', pathMatch: 'full', component: PageNotFoundComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(appRoutes, { preloadingStrategy: PreloadAllModules })],
    exports: [RouterModule],
    providers: [AuthGuardService,
        CanDeactivateGuardService,
        SessionService, CookieService]
})
export class AppRoutingModule { }


