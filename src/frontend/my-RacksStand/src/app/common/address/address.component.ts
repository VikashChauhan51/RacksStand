import { Component, Input, OnInit, HostListener } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, ReactiveFormsModule, NgForm } from '@angular/forms'
import { AddressService, AddressModel } from './address.service';
import {Helper,Country} from '../../core/helper';
const KEY_ESC = 27;
@Component({
  selector: 'app-address',
  templateUrl: './address.component.html'
})
export class AddressComponent implements OnInit {
    @Input() address: AddressModel = <AddressModel>{};
    isWating: boolean = false;
    negativeOnClick: (e: AddressModel) => void;
    positiveOnClick: (e: AddressModel) => void;
    private modalElement: any;
    countries:Country[] = Helper.Collection.getCountryies();
    private tempForm:NgForm=null;
    constructor(addressService: AddressService) {
        addressService.show = this.activate.bind(this);
    }
    activate(address: AddressModel, isEdit: boolean) {
        let promise = new Promise<AddressModel>((resolve, reject) => {
            this.negativeOnClick = (e: AddressModel) => resolve(e);
            this.positiveOnClick = (e: AddressModel) => resolve(e);
            this.address = address;
            if (!isEdit)
                this.address.Country = 0;
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
        this.isWating = true;
        this.positiveOnClick(this.address);
        this.reset();
    }
    cancel() {
        this.negativeOnClick(null);
        this.reset();
    }
    private reset() {
        this.modalElement.style.display = "none";
        this.isWating = false;
        this.address = <AddressModel>{};
        if(this.tempForm!=null){
            this.tempForm.reset();
            this.tempForm=null;
        }
      
    }
    private show() {
        this.modalElement.style.display = "block";
    }
    ngOnInit() {
        this.modalElement = document.getElementById('addressModal');
        this.address = <AddressModel>{};
        this.countries.unshift(<Country>{ id: 0, name: 'Please select' });
      
    }

    @HostListener('document:keyup', ['$event'])
    onKeyUp(ev: KeyboardEvent) {
        if (ev.keyCode === KEY_ESC) {
            this.cancel();
        }
    }

}
