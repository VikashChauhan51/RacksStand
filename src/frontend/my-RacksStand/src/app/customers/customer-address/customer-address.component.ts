import { Component, OnInit,Input,HostListener } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, ReactiveFormsModule, NgForm } from '@angular/forms'
import {Helper,Country} from '../../core/helper';
import { CustomerService } from '../shared/customer.service';
import { CustomerAddressModel } from '../shared/customer-model';
const KEY_ESC = 27;
@Component({
  selector: 'app-customer-address',
  templateUrl: './customer-address.component.html'
})
export class CustomerAddressComponent implements OnInit {

  @Input() customerAddress: CustomerAddressModel = <CustomerAddressModel>{};
  isWating: boolean = false;
  negativeOnClick: (e: CustomerAddressModel) => void;
  positiveOnClick: (e: CustomerAddressModel) => void;
  private modalElement: any;
  countries:Country[] = Helper.Collection.getCountryies();
  private tempForm:NgForm=null;
  constructor(addressService: CustomerService) {
      addressService.activate = this.activate.bind(this);
  }
  activate(address: CustomerAddressModel, isEdit: boolean) {
      let promise = new Promise<CustomerAddressModel>((resolve, reject) => {
          this.negativeOnClick = (e: CustomerAddressModel) => resolve(e);
          this.positiveOnClick = (e: CustomerAddressModel) => resolve(e);
          this.customerAddress = address;
          if (!isEdit)
              this.customerAddress.Country = 0;
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
      this.positiveOnClick(this.customerAddress);
      this.reset();
  }
  cancel() {
      this.negativeOnClick(null);
      this.reset();
  }
  private reset() {
      this.modalElement.style.display = "none";
      this.isWating = false;
      this.customerAddress = <CustomerAddressModel>{};
      if(this.tempForm!=null){
          this.tempForm.reset();
          this.tempForm=null;
      }
    
  }
  private show() {
      this.modalElement.style.display = "block";
  }
  ngOnInit() {
      this.modalElement = document.getElementById('customerAddressModal');
      this.customerAddress = <CustomerAddressModel>{};
      this.countries.unshift(<Country>{ id: 0, name: 'Please select' });
    
  }

  @HostListener('document:keyup', ['$event'])
  onKeyUp(ev: KeyboardEvent) {
      if (ev.keyCode === KEY_ESC) {
          this.cancel();
      }
  }

}
