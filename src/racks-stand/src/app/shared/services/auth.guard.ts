// src/app/auth/auth.guard.ts
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { TokenService } from './token.service'; // Import TokenService

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private tokenService: TokenService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | Promise<boolean> | boolean {

    // Check if the user is logged in
    const isLoggedIn = this.tokenService.isLoggedIn();

    // If user is logged in and trying to access signin or signup, redirect to home
    if (isLoggedIn && (route.routeConfig?.path === 'signin' || route.routeConfig?.path === 'signup')) {
      this.router.navigate(['/home']);  // Or redirect to any other page (e.g., '/dashboard')
      return false;
    }

    // If user is not logged in and trying to access protected routes, allow access
    if (!isLoggedIn && (route.routeConfig?.path === 'signin' || route.routeConfig?.path === 'signup')) {
      return true; // Allow access to signin and signup routes
    }

    // For all other routes, if user is logged in, allow access
    if (isLoggedIn) {
      return true;
    }

    // If the user is not logged in, prevent access and redirect to signin page
    this.router.navigate(['/signin']);
    return false;
  }
}
