import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from '../../shared/services/base.service';
import {Outcome} from '../../shared/services/outcome';
export type SignupRequest = {
  fullName: string;
  email: string;
  password: string;
  phoneNumber: string;
  companyName: string;
  address: AddressRequest;
};

export type AddressRequest = {
  address: string;
  address2?: string | null;
  city: string;
  state: string;
  country: string;
  zipCode: string;
};

@Injectable({
  providedIn: 'root',
})
export class SignUpService {


  constructor(private baseService: BaseService) {}

  // Signup new user
  signup(request:SignupRequest): Observable<Outcome<string>> {
    return this.baseService.post('api/v1/signup', request);
  }
}
