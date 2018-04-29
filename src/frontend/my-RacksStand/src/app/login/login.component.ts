import { Component, OnInit, OnDestroy, Input, OnChanges } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import {Observable} from 'rxjs/Rx';
import { LoginService } from './login.service';
import { SessionService } from '../core/session.service';
import { LoginModel } from './shared/login-model';
import { ToastService } from '../core/toast/toast.service';
import { SpinnerService } from '../core/spinner/spinner.service';
import { CookieService } from '../core/cookie.service';
import { Helper } from '../core/helper';
@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
    providers: [LoginService]
})
export class LoginComponent implements OnDestroy, OnInit {
    loginModel: LoginModel = new LoginModel();
    isWating: boolean = false;
    Loading:boolean=true;
    private loginSub: Subscription;
    private redirectTo: string;
    constructor(private router: Router, private activatedRoute: ActivatedRoute, private toastService: ToastService, private spinnerService: SpinnerService,
        private sessionService: SessionService, private loginService: LoginService, private cookieService: CookieService
    ) {
    }
    Login(isValid: boolean) {
        if (!isValid || this.isWating)
            return;

        this.loginSub = this.loginService
            .login(this.loginModel)
            .subscribe(result => {
                if (result.IsSucceed)
                {
                    this.sessionService.setSession(JSON.stringify(result.Data));
                    this.setCookieInfo();
                    let url = this.redirectTo ? this.redirectTo : '/dashboard';
                    this.router.navigate([url]);
                }
                  
                this.spinnerService.hide();
                this.isWating = false;
            },
            error => {
                this.spinnerService.hide();
                this.isWating = false;
                console.log('error:-' + error);
            });
        this.isWating = true;
        this.spinnerService.show();
    }
    

    ngOnInit() {
        this.activatedRoute.queryParams.subscribe(data => this.redirectTo = data['redirectTo']);
        if (this.sessionService.isLoggedIn()) 
            this.router.navigate(['dashboard']);
        else 
        {
            this.getCookieInfo();
            this.Loading=false;
        }

            
    }
    ngOnDestroy() {
        this.Loading=true;

    }
    private getCookieInfo() {
        this.loginModel.email = this.cookieService.getCookie(Helper.AppConfig.COOKIE_USERNAME_KEY);
        this.loginModel.password = this.cookieService.getCookie(Helper.AppConfig.COOKIE_PASSWORD_KEY);
        if (this.loginModel.email != undefined && this.loginModel.email != null && this.loginModel.email.length > 0)
            this.loginModel.rememberMe = true;
    }
    private setCookieInfo() {
        if (this.loginModel.rememberMe) {
            //set cookie value.
            this.cookieService.setCookie(Helper.AppConfig.COOKIE_USERNAME_KEY, this.loginModel.email, 10);
            this.cookieService.setCookie(Helper.AppConfig.COOKIE_PASSWORD_KEY, this.loginModel.password, 10);
        }
        else {
            //clear cookie value.
            this.cookieService.deleteCookie(Helper.AppConfig.COOKIE_USERNAME_KEY);
            this.cookieService.deleteCookie(Helper.AppConfig.COOKIE_PASSWORD_KEY);
        }
    }

   
}
