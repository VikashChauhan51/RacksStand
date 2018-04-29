import { Component, OnInit,Input,HostListener } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, ReactiveFormsModule, NgForm } from '@angular/forms'
import {Helper,Country} from '../../core/helper';
import { SupplierService } from '../shared/supplier.service';
import { SupplierAddressModel } from '../shared/supplier-model';
const KEY_ESC = 27;
@Component({
  selector: 'app-supplier-address',
  templateUrl: './supplier-address.component.html'
})
export class SupplierAddressComponent implements OnInit {

  @Input() supplierAddress: SupplierAddressModel = <SupplierAddressModel>{};
  isWating: boolean = false;
  negativeOnClick: (e: SupplierAddressModel) => void;
  positiveOnClick: (e: SupplierAddressModel) => void;
  private modalElement: any;
  countries:Country[] = Helper.Collection.getCountryies();
  private tempForm:NgForm=null;
  constructor(addressService: SupplierService) {
      addressService.activate = this.activate.bind(this);
  }
  activate(address: SupplierAddressModel, isEdit: boolean) {
      let promise = new Promise<SupplierAddressModel>((resolve, reject) => {
          this.negativeOnClick = (e: SupplierAddressModel) => resolve(e);
          this.positiveOnClick = (e: SupplierAddressModel) => resolve(e);
          this.supplierAddress = address;
          if (!isEdit)
              this.supplierAddress.Country = 0;
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
      this.positiveOnClick(this.supplierAddress);
      this.reset();
  }
  cancel() {
      this.negativeOnClick(null);
      this.reset();
  }
  private reset() {
      this.modalElement.style.display = "none";
      this.isWating = false;
      this.supplierAddress = <SupplierAddressModel>{};
      if(this.tempForm!=null){
          this.tempForm.reset();
          this.tempForm=null;
      }
    
  }
  private show() {
      this.modalElement.style.display = "block";
  }
  ngOnInit() {
      this.modalElement = document.getElementById('supplierAddressModal');
      this.supplierAddress = <SupplierAddressModel>{};
      this.countries.unshift(<Country>{ id: 0, name: 'Please select' });
    
  }

  @HostListener('document:keyup', ['$event'])
  onKeyUp(ev: KeyboardEvent) {
      if (ev.keyCode === KEY_ESC) {
          this.cancel();
      }
  }

}
