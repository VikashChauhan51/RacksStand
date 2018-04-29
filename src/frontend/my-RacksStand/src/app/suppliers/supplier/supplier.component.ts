import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, ReactiveFormsModule, NgForm } from '@angular/forms'
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import { SupplierService } from '../shared/supplier.service';
import { SupplierModel, SupplierAddressModel } from '../shared/supplier-model';
import { SpinnerService } from '../../core/spinner/spinner.service';
import { ModalService, ModalType } from '../../core/modal/modal.service';
import { ToastService } from '../../core/toast/toast.service';
import { AddressModel, AddressService } from '../../common/address/address.service';
import {Helper}  from '../../core/helper';
@Component({
    selector: 'app-supplier',
    templateUrl: './supplier.component.html'
})
export class SupplierComponent implements OnInit, OnDestroy {
    //#region "properties & variables"
    @Input() supplier: SupplierModel = <SupplierModel>{};
    private isEdit: boolean = false;
    isWating: boolean = false;
    loading:boolean=false;
    private supplierSub: Subscription;
    negativeOnClick: (e: SupplierModel) => void;
    positiveOnClick: (e: SupplierModel) => void;
    modalElement: any;
    private tempForm:NgForm=null;
    //#endregion "properties & variables"

     //#region "ctor"
    constructor(private router: Router, private route: ActivatedRoute, private supplierService: SupplierService,
        private spinnerService: SpinnerService, private toastService: ToastService, private modalService: ModalService)
    { supplierService.show = this.activate.bind(this); }
    //#endregion "ctor"

    //#region "public"
    activate(supplier: SupplierModel, isEdit: boolean) {
        let promise = new Promise<SupplierModel>((resolve, reject) => {
            this.negativeOnClick = (e: SupplierModel) => resolve(e);
            this.positiveOnClick = (e: SupplierModel) => resolve(e);
            this.isEdit = isEdit;
            this.supplier = supplier;
            if(this.supplier==undefined || this.supplier==null)
            this.supplier.Addresses = [];
            if(this.isEdit)
            this.getSupplier(this.supplier.Id);
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
        let _supplier = JSON.parse(JSON.stringify(this.supplier));
        if (this.isEdit)
            this.update(_supplier);
        else
            this.add(_supplier);

    }
    

    onAddAddress() {
        let supplierAddress = new SupplierAddressModel();
        supplierAddress.Id = Helper.GUID.NewID().replace(/-/g, '');
        this.supplierService.activate(<SupplierAddressModel>supplierAddress, false).then(e => {
            
            if (e != null)
                this.supplier.Addresses.unshift(e);
        }
        );

    }
    cancel() {
    this.reset();
    }
    onEditAddress(address: SupplierAddressModel) {
        //copy object into new one to void reference.
        let item = <SupplierAddressModel>{
            Id: address.Id,
            City: address.City,
            Country: address.Country,
            SupplierId: address.SupplierId,
            Email: address.Email,
            Fax: address.Fax,
            Phone: address.Phone,
            Remark: address.Remark,
            State: address.State,
            Status: address.Status,
            Street: address.Street,
            ZipCode: address.ZipCode
        };

        this.supplierService.activate(item, true).then(e => {
            if (e != null) {
                let index = this.supplier.Addresses.findIndex(x => x.Id == e.Id);
                if (index >= 0) {
                    this.supplier.Addresses.splice(index, 1, e);
                }
            }
        }
        );
    }

    onDeleteAddress(address: SupplierAddressModel) {
        this.modalService.activate( 'Are you sure you want to delete this?').then(e => {

            if (e == true && address != null) {
                let index = this.supplier.Addresses.indexOf(address);
                if (index != null && index >= 0)                 
                this.supplier.Addresses.splice(index, 1);
            }
        });
    }
    //#endregion "public"

    //#region "ngEvents"
    ngOnInit() {
        this.modalElement = document.getElementById('supplier');
        this.supplier = <SupplierModel>{};
    }
    ngOnDestroy() {
    }
    //#endregion "ngEvents"
    
    //#region "private"
    private show() {
        this.modalElement.style.display = "block";
    }
    private getSupplier(id:string) {
        if (this.loading)
              return;
          this.supplierSub = this.supplierService
              .getSupplier(id).finally(() => this.loading = false)
              .subscribe(result => {
                if (result.IsSucceed) {
                    this.supplier=result.Data;
                }
                 
              },
              error => {
                  this.errorAction(error);
              });
          this.loading = true;
      }
    private add(supplier: SupplierModel) {
        this.supplierSub = this.supplierService
            .save(supplier).finally(() => this.reset())
            .subscribe(result => {
                this.toastService.activate(result.Message);
               
                if (result.IsSucceed) {
                    this.positiveOnClick(this.supplier);
                }
            },
            error => {
                this.negativeOnClick(null);
                this.errorAction(error);
            });
    }
    private update(supplier: SupplierModel) {
        this.supplierSub = this.supplierService
            .update(supplier).finally(() => this.reset())
            .subscribe(result => {
                this.toastService.activate(result.Message);
                
                 if (result.IsSucceed) {
                    this.positiveOnClick(this.supplier);
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
        this.supplier = <SupplierModel>{};
        this.supplier.Addresses = [];
        if (this.supplierSub != undefined && this.supplierSub!=null)
            this.supplierSub.unsubscribe();
        if(this.tempForm!=null){
            this.tempForm.reset();
            this.tempForm=null;
        }
        this.supplier.Id = Helper.GUID.NewID().replace(/-/g, '');
    }
    
    private errorAction(error: any) {
        if (error.status == 0)
            this.router.navigate(['error']);
        else
            this.toastService.activate(error.message);
    }
    //#endregion "private"
}
