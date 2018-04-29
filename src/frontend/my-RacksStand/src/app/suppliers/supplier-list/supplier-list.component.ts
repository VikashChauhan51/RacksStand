import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { Helper } from '../../core/helper';
import { SupplierModel } from '../shared/supplier-model';
import { SupplierService } from '../shared/supplier.service';
import { ModalService, ModalType } from '../../core/modal/modal.service';
import { ToastService } from '../../core/toast/toast.service';
import { SpinnerService } from '../../core/spinner/spinner.service';
import { ActivatedRoute, Router } from '@angular/router';
import { SupplierSearchFilter } from '../shared/supplier-search-filter'
@Component({
    selector: 'app-supplier-list',
    templateUrl: './supplier-list.component.html'
})
export class SupplierListComponent implements OnInit, OnDestroy {
    isWating: boolean = false;
    private supplierSub: Subscription;
    suppliers: SupplierModel[] = [];
    status: number = 0;
    filter: SupplierSearchFilter = <SupplierSearchFilter>{ Keyword: '*', Take: 100, Start: 0, Status: null, CompanyId: null, UserId: null }
    hasNextPage: boolean = true;
    constructor(private supplierService: SupplierService, private spinnerService: SpinnerService, private toastService: ToastService,
        private modalService: ModalService, private router: Router, private activatedRoute: ActivatedRoute) { }

    ngOnInit() {
        this.suppliers = [];
        this.getSuppliers(this.copyFilter(this.filter));
    }
    ngOnDestroy() {
        if (this.supplierSub != undefined && this.supplierSub != null)
            this.supplierSub.unsubscribe();
    }
    onAdd() {
        let supplier = new SupplierModel();
        supplier.Addresses = [];
        supplier.Id = Helper.GUID.NewID().replace(/-/g, '');
        this.supplierService.show(supplier, false).then(x => {
            if (x != null) {
                {
                    this.reset();
                    this.getSuppliers(this.copyFilter(this.filter));
                }
            }
        });
    }
    onEdit(supplier: SupplierModel) {
        //copy object into new one to void reference.
        let item = <SupplierModel>{
            Id: supplier.Id,
            CompanyId: supplier.CompanyId
        };

        this.supplierService.show(item, true).then(e => {
            if (e != null) {
                let index = this.suppliers.findIndex(x => x.Id == e.Id);
                if (index >= 0) {
                    this.suppliers.splice(index, 1, e);
                }
            }
        });
    }
    onDelete(supplier: SupplierModel) {
        if (this.isWating)
            return;
        this.modalService.activate('Are you sure you want to delete this?').then(e => {

            if (e == true && supplier != null)
                this.delete(supplier);
        });
    }

    onSearch(event: string) {
        this.filter.Keyword = event.length > 0 ? event : '*';
        this.filter.Start = 0;
        this.suppliers = [];
        this.getSuppliers(this.copyFilter(this.filter));
    }
    onStatusChange(status: number) {
        this.status = status;
        this.filter.Start = 0;
        this.filter.Status = this.status > 0 ? this.status : null;
        this.suppliers = [];
        this.getSuppliers(this.copyFilter(this.filter));
    }
    loadMore() {
        this.filter.Start = this.suppliers.length;
        this.getSuppliers(this.copyFilter(this.filter));
    }

    private getSuppliers(filter: SupplierSearchFilter) {
        if (this.isWating)
            return;
        this.supplierSub = this.supplierService
            .get(filter).finally(() => this.isWating = false)
            .subscribe(response => {
                if (response.Data != null) {
                    let result = [...this.suppliers, ...response.Data];
                    this.suppliers = result;
                    this.hasNextPage = (response.Data.length <= 0 || this.suppliers.length < this.filter.Take) ? false : true;
                }
            },
            error => {

                this.errorAction(error);
            });
        this.isWating = true;
    }
    private delete(supplier: SupplierModel) {

        this.supplierSub = this.supplierService.delete(supplier.Id).finally(() => this.isWating = false).subscribe(result => {
            if (result.IsSucceed) {
                let index = this.suppliers.indexOf(supplier);
                if (index != null && index >= 0)
                    this.suppliers.splice(index, 1);
            }

        },
            error => {
                this.errorAction(error);
            });
        this.isWating = true;
    }
    private copyFilter(filter: SupplierSearchFilter): SupplierSearchFilter {
        //copy object into new one to void reference.
        let item = <SupplierSearchFilter>{
            Keyword: filter.Keyword,
            Start: filter.Start,
            Take: filter.Take,
            Status: filter.Status,
            CompanyId: filter.CompanyId,
            UserId: filter.UserId
        };
        return item;
    }
    private reset() {
        this.isWating = false;
        this.hasNextPage = true;
        this.suppliers = [];
        this.filter = <SupplierSearchFilter>{ Keyword: '*', Take: 100, Start: 0, Status: null, CompanyId: null, UserId: null }
        this.status = 0;
    }
    private errorAction(error: any) {
        if (error.status == 0)
            this.router.navigate(['error']);
        else
            this.toastService.activate(error.message);
    }
}

