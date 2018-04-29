import { Component, OnInit, OnDestroy, Input, OnChanges } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import {Observable} from 'rxjs/Rx';
import { LoginModel } from '../shared/login-model';
import { LoginService } from '../login.service';
import { ToastService } from '../../core/toast/toast.service';
import { SpinnerService } from '../../core/spinner/spinner.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css'],
  providers: [LoginService]
})
export class ResetPasswordComponent implements OnInit {
    resetModel: LoginModel = new LoginModel();
    isWating: boolean = false;
    private resetSub: Subscription;
    private token: string = '';
    constructor(private router: Router, private activatedRoute: ActivatedRoute, private toastService: ToastService, private spinnerService: SpinnerService,
        private loginService: LoginService) {
    }
    Reset(isValid: boolean) {
        
        if (!isValid || this.isWating)
            return;

        this.resetSub = this.loginService
            .reset(this.resetModel.password, this.token)
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
        this.activatedRoute.queryParams.subscribe( data => this.token = data['token']);
       
  }

}
