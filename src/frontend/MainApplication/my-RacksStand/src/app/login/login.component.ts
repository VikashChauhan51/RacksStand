import { Component, OnInit,OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import {Observable} from 'rxjs/Rx';
import { LoginService } from './login.service';
import { SessionService } from '../core/session.service';
 

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
    providers: [LoginService]
})
export class LoginComponent implements  OnDestroy {

  private loginSub: Subscription;
   constructor(
    private loginService: LoginService,
    private route: ActivatedRoute,
    private router: Router,
    private sessionService: SessionService) {
  }

  ngOnInit() {
  }


  public get isLoggedIn() : boolean {
    return this.sessionService.isLoggedIn;
  }

  login() {
      debugger;
    this.loginSub = this.loginService
      .login()
      .mergeMap(loginResult => this.route.queryParams)
      .map(qp => qp['redirectTo'])
      .subscribe(redirectTo => {
        
        if (this.sessionService.isLoggedIn) {
          let url = redirectTo ? [redirectTo] : [ '/home' ];
          this.router.navigate(url);
        }
      });
  }

  logout() {
    this.loginService.logout();
   
  }

  ngOnDestroy() {
    if (this.loginSub) {
      this.loginSub.unsubscribe();
    }
  }
}
