import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { SessionService } from '../../app/core/session.service';

@Injectable()
export class LoginService {

  constructor( private sessionService: SessionService) { }
  login() {
    return Observable.of(true)
        .delay(1000)
        .do(this.toggleLogState.bind(this));
        
  }

  logout() {
    this.toggleLogState(false);
  }

  private toggleLogState(val: boolean) {
    this.sessionService.isLoggedIn = val;
  }
}
