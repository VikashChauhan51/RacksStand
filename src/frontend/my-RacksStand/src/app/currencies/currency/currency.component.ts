import { Component, Input, OnDestroy, OnInit, HostListener } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, ReactiveFormsModule, NgForm } from '@angular/forms'
import { CurrencyService } from '../shared/currency.service';
import { CurrencyModel } from '../shared/currency-model';
import { Helper } from '../../core/helper';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../core/toast/toast.service';
import { Subscription } from 'rxjs/Subscription';
const KEY_ESC = 27;

@Component({
    selector: 'app-currency',
    templateUrl: './currency.component.html'
})
export class CurrencyComponent implements OnInit, OnDestroy {
    @Input() currency: CurrencyModel = <CurrencyModel>{};
    isWating: boolean = false;
    negativeOnClick: (e: CurrencyModel) => void;
    positiveOnClick: (e: CurrencyModel) => void;
    private currencySub: Subscription;
    isEdit: boolean = false;
    modalElement: any;
    private tempForm:NgForm=null;
    constructor(private currencyService: CurrencyService, private router: Router, private activatedRoute: ActivatedRoute, private toastService: ToastService) {
        currencyService.show = this.activate.bind(this);
    }
    activate(currency: CurrencyModel, isEdit: boolean) {
        let promise = new Promise<CurrencyModel>((resolve, reject) => {
            this.negativeOnClick = (e: CurrencyModel) => resolve(e);
            this.positiveOnClick = (e: CurrencyModel) => resolve(e);
            this.isEdit = isEdit;
            this.currency = currency;
            this.show();
        });

        return promise;
    }
    save(form: NgForm) {
        this.tempForm=form;
        let isValid = form.valid;
        if (!isValid || this.isWating) {
            Object.keys(form.controls).forEach(key => {
                (form.controls[key] as FormControl).markAsTouched();
            });
            return;
        }


        //copy JavaScript object to new variable NOT by reference.
        let _currency = JSON.parse(JSON.stringify(this.currency));
        if (!this.isEdit)
            this.add(_currency);
        else
            this.update(_currency);
     

    }
    private add(currency: CurrencyModel) {
        this.isWating = true;
        this.currencySub = this.currencyService.save(currency).finally(() => this.reset()).subscribe(result => {
            if (result.IsSucceed)
                this.positiveOnClick(currency);
            else
                this.negativeOnClick(null);

        },
            error => {

                this.negativeOnClick(null);
                this.errorAction(error);
            });

    }
    private update(currency: CurrencyModel) {
        this.isWating = true;
        this.currencySub = this.currencyService.update(currency).finally(() => this.reset()).subscribe(result => {
            if (result.IsSucceed)
                this.positiveOnClick(currency);
            else
                this.negativeOnClick(null);

        },
            error => {
                this.negativeOnClick(null);
                this.errorAction(error);
            });
    }
    cancel() {
        if (this.isWating)
            return;
        this.negativeOnClick(null);
        this.reset();
    }
    private reset() {
        this.modalElement.style.display = "none";
        this.isWating = false;
        this.isEdit = false;
        this.currency = <CurrencyModel>{};
        if (this.currencySub != undefined && this.currencySub!=null)
            this.currencySub.unsubscribe();
        if(this.tempForm!=null){
            this.tempForm.reset();
            this.tempForm=null;
        }
    }
    private show() {
        this.modalElement.style.display = "block";
    }
    ngOnInit() {
        this.modalElement = document.getElementById('currency');
        this.currency = <CurrencyModel>{};
    }
    ngOnDestroy() {
        this.reset();
    }
    //  @HostListener('document:keyup', ['$event'])
    //     onKeyUp(ev: KeyboardEvent) {
    //         if (ev.keyCode === KEY_ESC) {
    //             this.cancel();
    //         }
    //     }
    private errorAction(error: any) {
        if (error.status == 0)
            this.router.navigate(['error']);
        else
            this.toastService.activate(error.message);
    }
}
