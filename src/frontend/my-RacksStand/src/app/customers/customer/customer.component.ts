import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, ReactiveFormsModule, NgForm } from '@angular/forms'
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import { CustomerService } from '../shared/customer.service';
import { CustomerModel, CustomerAddressModel } from '../shared/customer-model';
import { SpinnerService } from '../../core/spinner/spinner.service';
import { ModalService, ModalType } from '../../core/modal/modal.service';
import { ToastService } from '../../core/toast/toast.service';
import {Helper}  from '../../core/helper';
@Component({
    selector: 'app-customer',
    templateUrl: './customer.component.html'
})
export class CustomerComponent implements OnInit, OnDestroy {
    //#region "properties & variables"
    @Input() customer: CustomerModel = <CustomerModel>{};
    private isEdit: boolean = false;
    isWating: boolean = false;
    loading:boolean=false;
    private customerSub: Subscription;
    negativeOnClick: (e: CustomerModel) => void;
    positiveOnClick: (e: CustomerModel) => void;
    modalElement: any;
    private tempForm:NgForm=null;
    //#endregion "properties & variables"

     //#region "ctor"
    constructor(private router: Router, private route: ActivatedRoute, private customerService: CustomerService,
        private spinnerService: SpinnerService, private toastService: ToastService, private modalService: ModalService)
    { customerService.show = this.activate.bind(this); }
    //#endregion "ctor"

    //#region "public"
    activate(customer: CustomerModel, isEdit: boolean) {
        let promise = new Promise<CustomerModel>((resolve, reject) => {
            this.negativeOnClick = (e: CustomerModel) => resolve(e);
            this.positiveOnClick = (e: CustomerModel) => resolve(e);
            this.isEdit = isEdit;
            this.customer = customer;
            if(this.customer==undefined || this.customer==null)
            this.customer.Addresses = [];
            if(this.isEdit)
            this.getCustomer(this.customer.Id);
            this.show();
        });

        return promise;
    }
    save(form: NgForm) {
        //validate form.
        this.tempForm=form;
        let isValid = form.valid;
        if (!isValid || this.isWating) {
            Object.keys(form.controls).forEach(key => {
                (form.controls[key] as FormControl).markAsTouched();
            });
            return;
        }
        this.isWating = true;
        //copy JavaScript object to new variable NOT by reference.
        let _customer = JSON.parse(JSON.stringify(this.customer));
        if (this.isEdit)
            this.update(_customer);
        else
            this.add(_customer);

    }
    

    onAddAddress() {
        let customerAddress = new CustomerAddressModel();
        customerAddress.Id = Helper.GUID.NewID().replace(/-/g, '');
        this.customerService.activate(<CustomerAddressModel>customerAddress, false).then(e => {
            
            if (e != null)
                this.customer.Addresses.unshift(e);
        }
        );

    }
    cancel() {
    this.reset();
    }
    onEditAddress(address: CustomerAddressModel) {
        //copy object into new one to void reference.
        let item = <CustomerAddressModel>{
            Id: address.Id,
            City: address.City,
            Country: address.Country,
            CustomerId: address.CustomerId,
            Email: address.Email,
            Fax: address.Fax,
            Phone: address.Phone,
            Remark: address.Remark,
            State: address.State,
            Status: address.Status,
            Street: address.Street,
            ZipCode: address.ZipCode
        };

        this.customerService.activate(item, true).then(e => {
            if (e != null) {
                let index = this.customer.Addresses.findIndex(x => x.Id == e.Id);
                if (index >= 0) {
                    this.customer.Addresses.splice(index, 1, e);
                }
            }
        }
        );
    }

    onDeleteAddress(address: CustomerAddressModel) {
        this.modalService.activate( 'Are you sure you want to delete this?').then(e => {

            if (e == true && address != null) {
                let index = this.customer.Addresses.indexOf(address);
                if (index != null && index >= 0)                 
                this.customer.Addresses.splice(index, 1);
            }
        });
    }
    //#endregion "public"

    //#region "ngEvents"
    ngOnInit() {
        this.modalElement = document.getElementById('customer');
        this.customer = <CustomerModel>{};
    }
    ngOnDestroy() {
    }
    //#endregion "ngEvents"
    
    //#region "private"
    private show() {
        this.modalElement.style.display = "block";
    }
    private getCustomer(id:string) {
        if (this.loading)
              return;
          this.customerSub = this.customerService
              .getCustomer(id).finally(() => this.loading = false)
              .subscribe(result => {
                if (result.IsSucceed) {
                    this.customer=result.Data;
                }
                 
              },
              error => {
                  this.errorAction(error);
              });
          this.loading = true;
      }
    private add(customer: CustomerModel) {
        this.customerSub = this.customerService
            .save(customer).finally(() => this.reset())
            .subscribe(result => {
                this.toastService.activate(result.Message);
               
                if (result.IsSucceed) {
                    this.positiveOnClick(this.customer);
                }
            },
            error => {
                this.negativeOnClick(null);
                this.errorAction(error);
            });
    }
    private update(customer: CustomerModel) {
        this.customerSub = this.customerService
            .update(customer).finally(() => this.reset())
            .subscribe(result => {
                this.toastService.activate(result.Message);
                
                 if (result.IsSucceed) {
                    this.positiveOnClick(this.customer);
                }
            },
            error => {  
                this.negativeOnClick(null);
                this.errorAction(error);
            });
    }
     
    private reset() {
        this.modalElement.style.display = "none";
        this.isWating = false;
        this.isEdit = false;
        this.customer = <CustomerModel>{};
        this.customer.Addresses = [];
        if (this.customerSub != undefined && this.customerSub!=null)
            this.customerSub.unsubscribe();
        if(this.tempForm!=null){
            this.tempForm.reset();
            this.tempForm=null;
        }
        this.customer.Id = Helper.GUID.NewID().replace(/-/g, '');
    }
    
    private errorAction(error: any) {
        if (error.status == 0)
            this.router.navigate(['error']);
        else
            this.toastService.activate(error.message);
    }
    //#endregion "private"
}
