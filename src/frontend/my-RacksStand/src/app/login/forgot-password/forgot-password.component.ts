import { Component, OnInit, OnDestroy, Input, OnChanges } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import {Observable} from 'rxjs/Rx';
import { LoginModel } from '../shared/login-model';
import { LoginService } from '../login.service';
import { SessionService } from '../../core/session.service';
import { ToastService } from '../../core/toast/toast.service';
import { SpinnerService } from '../../core/spinner/spinner.service';


@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css'],
  providers: [LoginService]
})
export class ForgotPasswordComponent implements OnInit {
    forgotModel: LoginModel = new LoginModel();
    isWating: boolean = false;
    private forgotSub: Subscription;

    constructor(private router: Router, private toastService: ToastService, private spinnerService: SpinnerService,
        private sessionService: SessionService, private loginService: LoginService 
    )
    {
    }
    Forgot(isValid: boolean) {
        if (!isValid || this.isWating)
            return;

        this.forgotSub = this.loginService
            .forgot(this.forgotModel.email)
            .subscribe(result => {
                this.toastService.activate(result.Message);
                this.spinnerService.hide();
                this.isWating = false;
            },
            error => {
                console.log('error:-' + error);
            });
        this.isWating = true;
        this.spinnerService.show();
    }
    ngOnInit() {
        if (this.sessionService.isLoggedIn())
            this.router.navigate(['dashboard']);
  }

}
