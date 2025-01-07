import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private baseService: BaseService) {}

  // Login user
  login(email: string, password: string): Observable<any> {
    return this.baseService.post('api/v1/signin', { email, password });
  }

  // Register user
  register(data: any): Observable<any> {
    return this.baseService.post('api/v1/signup', data);
  }

  // Logout user
  logout(): Observable<any> {
    return this.baseService.post('api/v1/signout', {});
  }

  // Refresh token using refresh token
  refreshToken(refreshToken: string): Observable<any> {
    return this.baseService.post('api/v1/refresh-token', { refreshToken });
  }
}
