import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { Helper } from '../../core/helper';
import { CustomerModel } from '../shared/customer-model';
import { CustomerService } from '../shared/customer.service';
import { ModalService, ModalType } from '../../core/modal/modal.service';
import { ToastService } from '../../core/toast/toast.service';
import { SpinnerService } from '../../core/spinner/spinner.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerSearchFilter } from '../shared/customer-search-filter'
@Component({
    selector: 'app-customer-list',
    templateUrl: './customer-list.component.html'
})
export class CustomerListComponent implements OnInit, OnDestroy {
    isWating: boolean = false;
    private customerSub: Subscription;
    customers: CustomerModel[] = [];
    status: number = 0;
    filter: CustomerSearchFilter = <CustomerSearchFilter>{ Keyword: '*', Take: 100, Start: 0, Status: null, CompanyId: null, UserId: null }
    hasNextPage: boolean = true;
    constructor(private customerService: CustomerService, private spinnerService: SpinnerService, private toastService: ToastService,
        private modalService: ModalService, private router: Router, private activatedRoute: ActivatedRoute) { }

    ngOnInit() {
        this.customers = [];
        this.getCustomers(this.copyFilter(this.filter));
    }
    ngOnDestroy() {
        if (this.customerSub != undefined && this.customerSub != null)
            this.customerSub.unsubscribe();
    }
    onAdd() {
        let customer = new CustomerModel();
        customer.Addresses = [];
        customer.Id = Helper.GUID.NewID().replace(/-/g, '');
        this.customerService.show(customer, false).then(x => {
            if (x != null) {
                {
                    this.reset();
                    this.getCustomers(this.copyFilter(this.filter));
                }
            }
        });
    }
    onEdit(customer: CustomerModel) {
        //copy object into new one to void reference.
        let item = <CustomerModel>{
            Id: customer.Id,
            CompanyId: customer.CompanyId
        };

        this.customerService.show(item, true).then(e => {
            if (e != null) {
                let index = this.customers.findIndex(x => x.Id == e.Id);
                if (index >= 0) {
                    this.customers.splice(index, 1, e);
                }
            }
        });
    }
    onDelete(customer: CustomerModel) {
        if (this.isWating)
            return;
        this.modalService.activate('Are you sure you want to delete this?').then(e => {

            if (e == true && customer != null)
                this.delete(customer);
        });
    }

    onSearch(event: string) {
        this.filter.Keyword = event.length > 0 ? event : '*';
        this.filter.Start = 0;
        this.customers = [];
        this.getCustomers(this.copyFilter(this.filter));
    }
    onStatusChange(status: number) {
        this.status = status;
        this.filter.Start = 0;
        this.filter.Status = this.status > 0 ? this.status : null;
        this.customers = [];
        this.getCustomers(this.copyFilter(this.filter));
    }
    loadMore() {
        this.filter.Start = this.customers.length;
        this.getCustomers(this.copyFilter(this.filter));
    }

    private getCustomers(filter: CustomerSearchFilter) {
        if (this.isWating)
            return;
        this.customerSub = this.customerService
            .get(filter).finally(() => this.isWating = false)
            .subscribe(response => {
                if (response.Data != null) {
                    let result = [...this.customers, ...response.Data];
                    this.customers = result;
                    this.hasNextPage = (response.Data.length <= 0 || this.customers.length < this.filter.Take) ? false : true;
                }
            },
            error => {

                this.errorAction(error);
            });
        this.isWating = true;
    }
    private delete(customer: CustomerModel) {

        this.customerSub = this.customerService.delete(customer.Id).finally(() => this.isWating = false).subscribe(result => {
            if (result.IsSucceed) {
                let index = this.customers.indexOf(customer);
                if (index != null && index >= 0)
                    this.customers.splice(index, 1);
            }

        },
            error => {
                this.errorAction(error);
            });
        this.isWating = true;
    }
    private copyFilter(filter: CustomerSearchFilter): CustomerSearchFilter {
        //copy object into new one to void reference.
        let item = <CustomerSearchFilter>{
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
        this.customers = [];
        this.filter = <CustomerSearchFilter>{ Keyword: '*', Take: 100, Start: 0, Status: null, CompanyId: null, UserId: null }
        this.status = 0;
    }
    private errorAction(error: any) {
        if (error.status == 0)
            this.router.navigate(['error']);
        else
            this.toastService.activate(error.message);
    }
}

