import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private baseService: BaseService) {
  }

  login(email: string, password: string): Observable<any> {
    return this.baseService.post('api/v1/signin', { email, password });
  }

  register(data: any): Observable<any> {
    return this.baseService.post('signup', data);
  }

  logout(): Observable<any> {
    return this.baseService.post('logout', {});
  }
}
