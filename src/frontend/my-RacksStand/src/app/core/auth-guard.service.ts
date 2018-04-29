import { Injectable } from '@angular/core';
import {
  CanActivate,
  CanActivateChild,
  CanLoad,
  Route,
  Router,
  ActivatedRouteSnapshot,
  RouterStateSnapshot
} from '@angular/router';

import { SessionService } from './session.service';
@Injectable()
export class AuthGuardService implements CanActivate, CanActivateChild, CanLoad {
  constructor(private sessionService: SessionService, private router: Router) { }

  canLoad(route: Route) {
      let value = this.sessionService.isLoggedIn();
      if (value) 
        return true;
   
    let url = `/${route.path}`;
    this.router.navigate(['/login'], { queryParams: { redirectTo: url } });
    return value;
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ) {
      let value = this.sessionService.isLoggedIn();
      if (value)
        return true;
    
    this.router.navigate(['/login'], { queryParams: { redirectTo: state.url } });

    return value;
  }

  canActivateChild(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ) {
    return this.canActivate(route, state);
  }
}
