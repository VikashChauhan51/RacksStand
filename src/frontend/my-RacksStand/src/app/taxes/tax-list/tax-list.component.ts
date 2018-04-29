import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { Helper } from '../../core/helper';
import { TaxModel } from '../shared/tax-model';
import { TaxService } from '../shared/tax.service';
import { SpinnerService } from '../../core/spinner/spinner.service';
import { ModalService, ModalType } from '../../core/modal/modal.service';
import { ToastService } from '../../core/toast/toast.service';
import { ActivatedRoute, Router } from '@angular/router';
import { TaxSearchFilter } from '../shared/tax-search-filter'
@Component({
    selector: 'app-tax-list',
    templateUrl: './tax-list.component.html'
})
export class TaxListComponent implements OnInit, OnDestroy {
    isWating: boolean = false;
    private taxSub: Subscription;
    taxes: TaxModel[] = [];
    filter: TaxSearchFilter = <TaxSearchFilter>{ Keyword: '*', Take: 100, Start: 0, Active: null, IsCompound: null, CompanyId: null, UserId: null }
    status: number = 0;
    taxType: number = 0;
    hasNextPage: boolean = true;
    constructor(private taxService: TaxService, private spinnerService: SpinnerService, private toastService: ToastService,
        private modalService: ModalService, private router: Router, private activatedRoute: ActivatedRoute) { }

    ngOnInit() {
        this.taxes = [];
        this.getTaxes(this.copyFilter(this.filter));
    }
    onAdd() {
        let tax = new TaxModel();
        tax.Id = Helper.GUID.NewID().replace(/-/g, '');
        this.taxService.show(tax, false).then(x => {
            if (x != null) {
                this.reset();
                this.getTaxes(this.copyFilter(this.filter));
            }
        });
    }
    onEdit(tax: TaxModel) {
        //copy object into new one to void reference.
        let item = <TaxModel>{
            Id: tax.Id,
            Name: tax.Name,
            Code: tax.Code,
            CompanyId: tax.CompanyId,
            IsCompound: tax.IsCompound,
            Rate: tax.Rate,
            IsActive: tax.IsActive
        };

        this.taxService.show(item, true).then(e => {
            if (e != null) {
                let index = this.taxes.findIndex(x => x.Id == e.Id);
                if (index >= 0) {
                    this.taxes.splice(index, 1, e);
                }
            }

        });
    }
    onDelete(tax: TaxModel) {
        if (this.isWating)
            return;
        this.modalService.activate('Are you sure you want to delete this?').then(e => {

            if (e == true && tax != null)
                this.delete(tax);
        });
    }
    onSearch(event: string) {
        this.filter.Keyword = event.length > 0 ? event : '*';
        this.filter.Start = 0;
        this.taxes = [];
        this.getTaxes(this.copyFilter(this.filter));
    }
    onStatusChange(status: number) {
        this.status = status;
        this.filter.Start = 0;
        switch (this.status) {
            case 0:
                this.filter.Active = null;
                break;
            case 1:
                this.filter.Active = true;
                break;
            case 2:
                this.filter.Active = false;
                break;
        }
        this.taxes = [];
        this.getTaxes(this.copyFilter(this.filter));
    }
    onTaxChange(type: number) {
        this.taxType = type;
        this.filter.Start = 0;
        switch (this.taxType) {
            case 0:
                this.filter.IsCompound = null;
                break;
            case 1:
                this.filter.IsCompound = true;
                break;
            case 2:
                this.filter.IsCompound = false;
                break;
        }

        this.taxes = [];
        this.getTaxes(this.copyFilter(this.filter));
    }
    ngOnDestroy() {
        if (this.taxSub != undefined)
            this.taxSub.unsubscribe();
    }
    loadMore() {
        this.filter.Start = this.taxes.length;
        this.getTaxes(this.copyFilter(this.filter));
    }

    private delete(tax: TaxModel) {
        this.isWating = true;
        this.taxSub = this.taxService.delete(tax.Id).subscribe(result => {
            this.isWating = false;
            this.spinnerService.hide();
            if (result.IsSucceed) {
                let index = this.taxes.indexOf(tax);
                if (index != null && index >= 0)
                    this.taxes.splice(index, 1);
            }
        },
            error => {
                this.isWating = false;
                this.spinnerService.hide();
                this.errorAction(error);
            });
        this.spinnerService.show();
    }
    private copyFilter(filter: TaxSearchFilter): TaxSearchFilter {
        //copy object into new one to void reference.
        let item = <TaxSearchFilter>{
            Keyword: filter.Keyword,
            Start: filter.Start,
            Take: filter.Take,
            Active: filter.Active,
            IsCompound: filter.IsCompound,
            CompanyId: filter.CompanyId,
            UserId: filter.UserId
        };
        return item;
    }
    private getTaxes(filter: TaxSearchFilter) {
        if (this.isWating)
            return;
        this.taxSub = this.taxService
            .get(filter).finally(() => this.isWating = false)
            .subscribe(response => {
                if (response.Data != null) {
                    let result = [...this.taxes, ...response.Data];
                    this.taxes = result;
                    this.hasNextPage = (response.Data.length <= 0 || this.taxes.length < this.filter.Take) ? false : true;
                }
            },
            error => this.errorAction(error));
        this.isWating = true;

    }
    private errorAction(error: any) {
        if (error.status == 0)
            this.router.navigate(['error']);
        else
            this.toastService.activate(error.message);
    }
    private reset() {
        this.isWating = false;
        this.hasNextPage = true;
        this.status = 0;
        this.taxType = 0;
        this.taxes = [];
        this.filter = <TaxSearchFilter>{ Keyword: '*', Take: 100, Start: 0, Active: null, IsCompound: null, CompanyId: null, UserId: null }
    }
}
