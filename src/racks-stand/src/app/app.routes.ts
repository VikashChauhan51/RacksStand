import { Routes } from '@angular/router';
import { AuthGuard } from './shared/services/auth.guard';

export const routes: Routes = [
  { path: 'home', loadComponent: () => import('./home/home.component').then(m => m.HomeComponent)},
  { path: 'about', loadComponent: () => import('./about/about.component').then(m => m.AboutComponent) },
  { path: 'contact', loadComponent: () => import('./contact/contact.component').then(m => m.ContactComponent)},
  { path: 'signin', loadComponent: () => import('./user/login/login.component').then(m => m.LoginComponent),canActivate: [AuthGuard] },
  { path: 'signup', loadComponent: () => import('./user/signup/signup.component').then(m => m.SignupComponent),canActivate: [AuthGuard] },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', redirectTo: '/home' },
];
