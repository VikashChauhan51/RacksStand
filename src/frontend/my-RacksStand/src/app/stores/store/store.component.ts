import { Component, ViewChild, Input, OnDestroy, OnInit, HostListener } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, ReactiveFormsModule, NgForm } from '@angular/forms'
import { StoreService } from '../shared/store.service';
import { StoreModel } from '../shared/store-model';
import { Helper, Country } from '../../core/helper';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../core/toast/toast.service';
import { Subscription } from 'rxjs/Subscription';
const KEY_ESC = 27;

@Component({
    selector: 'app-store',
    templateUrl: './store.component.html'
})
export class StoreComponent implements OnInit, OnDestroy {
    @Input() store: StoreModel = <StoreModel>{};
    isWating: boolean = false;
    negativeOnClick: (e: StoreModel) => void;
    positiveOnClick: (e: StoreModel) => void;
    private storeSub: Subscription;
    isEdit: boolean = false;
    countries: Country[] = Helper.Collection.getCountryies();
    modalElement: any;
    @ViewChild('storeForm') public storeForm: NgForm;
    constructor(private storeService: StoreService, private router: Router, private activatedRoute: ActivatedRoute, private toastService: ToastService) {
        storeService.show = this.activate.bind(this);
    }
    activate(store: StoreModel, isEdit: boolean) {
        let promise = new Promise<StoreModel>((resolve, reject) => {
            this.negativeOnClick = (e: StoreModel) => resolve(e);
            this.positiveOnClick = (e: StoreModel) => resolve(e);
            this.isEdit = isEdit;
            this.store = store;
            this.show();
        });

        return promise;
    }
    save(form: NgForm) {
        let isValid = form.valid;
        if (!isValid || this.isWating) {
            Object.keys(form.controls).forEach(key => {
                (form.controls[key] as FormControl).markAsTouched();
            });
            return;
        }


        //copy JavaScript object to new variable NOT by reference.
        let _store = JSON.parse(JSON.stringify(this.store));
        if (!this.isEdit)
            this.add(_store);
        else
            this.update(_store);


    }
    private add(store: StoreModel) {
        this.isWating = true;
        this.storeSub = this.storeService.save(store).finally(() => this.reset()).subscribe(result => {
            if (result.IsSucceed)
                this.positiveOnClick(store);
            else
                this.negativeOnClick(null);

        },
            error => {

                this.negativeOnClick(null);
                this.errorAction(error);
            });

    }
    private update(store: StoreModel) {
        this.isWating = true;
        this.storeSub = this.storeService.update(store).finally(() => this.reset()).subscribe(result => {
            if (result.IsSucceed)
                this.positiveOnClick(store);
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
        this.store = <StoreModel>{};
        if (this.storeSub != undefined && this.storeSub != null)
            this.storeSub.unsubscribe();
            this.storeForm.reset();
    }
    private show() {
        this.modalElement.style.display = "block";
    }
    ngOnInit() {
        this.modalElement = document.getElementById('store');
        this.store = <StoreModel>{};
        this.countries.unshift(<Country>{ id: 0, name: 'Please select' });
    }
    ngOnDestroy() {
        this.reset();
    }
    
    private errorAction(error: any) {
        if (error.status == 0)
            this.router.navigate(['error']);
        else
            this.toastService.activate(error.message);
    }
}
