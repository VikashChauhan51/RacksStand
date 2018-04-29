import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { Helper } from '../../core/helper';
import { CurrencyModel } from '../shared/currency-model';
import { CurrencyService } from '../shared/currency.service';
import { SpinnerService } from '../../core/spinner/spinner.service';
import { ModalService, ModalType } from '../../core/modal/modal.service';
import { ToastService } from '../../core/toast/toast.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CurrencySearchFilter } from '../shared/currency-search-filter'
@Component({
    selector: 'app-currency-list',
    templateUrl: './currency-list.component.html'
})
export class CurrencyListComponent implements OnInit, OnDestroy {
    isWating: boolean = false;
    private currencySub: Subscription;
    currencies: CurrencyModel[] = [];
    filter: CurrencySearchFilter = <CurrencySearchFilter>{ Keyword: '*', Take: 100, Start: 0, Active: null, CompanyId: null, UserId: null }
    status: number = 0;
    hasNextPage: boolean = true;
    constructor(private currencyService: CurrencyService, private spinnerService: SpinnerService, private toastService: ToastService,
        private modalService: ModalService, private router: Router, private activatedRoute: ActivatedRoute) { }

    ngOnInit() {
        this.currencies = [];
        this.getCurrencies(this.copyFilter(this.filter));
    }
    onSearch(event: string) {
        this.filter.Keyword = event.length > 0 ? event : '*';
        this.filter.Start = 0;
        this.currencies = [];
        this.getCurrencies(this.copyFilter(this.filter));
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
        this.currencies = [];
        this.getCurrencies(this.copyFilter(this.filter));
    }
    loadMore() {
        this.filter.Start = this.currencies.length;
        this.getCurrencies(this.copyFilter(this.filter));
    }
    onAdd() {
        let currency = new CurrencyModel();
        currency.Id = Helper.GUID.NewID().replace(/-/g, '');
        this.currencyService.show(currency, false).then(x => {
            if (x != null) {
                {
                    this.reset();
                    this.getCurrencies(this.copyFilter(this.filter));
                }
            }
        });
    }
    onEdit(currency: CurrencyModel) {
        //copy object into new one to void reference.
        let item = <CurrencyModel>{
            Id: currency.Id,
            Name: currency.Name,
            Code: currency.Code,
            CompanyId: currency.CompanyId,
            Symbol: currency.Symbol,
            ExchangeRate: currency.ExchangeRate,
            IsActive: currency.IsActive
        };

        this.currencyService.show(item, true).then(e => {
            if (e != null) {
                let index = this.currencies.findIndex(x => x.Id == e.Id);
                if (index >= 0) {
                    this.currencies.splice(index, 1, e);
                }
            }

        });
    }
    onDelete(currency: CurrencyModel) {
        if (this.isWating)
            return;
        this.modalService.activate('Are you sure you want to delete this?').then(e => {

            if (e == true && currency != null)
                this.delete(currency);
        });
    }
    ngOnDestroy() {
        if (this.currencySub != undefined && this.currencySub != null)
            this.currencySub.unsubscribe();
    }
    private delete(currency: CurrencyModel) {
        this.isWating = true;
        this.currencySub = this.currencyService.delete(currency.Id).subscribe(result => {
            this.isWating = false;
            this.spinnerService.hide();
            if (result.IsSucceed) {
                let index = this.currencies.indexOf(currency);
                if (index != null && index >= 0)
                    this.currencies.splice(index, 1);
            }
        },
            error => {
                this.isWating = false;
                this.spinnerService.hide();
                this.errorAction(error);
            });
        this.spinnerService.show();
    }
    private copyFilter(filter: CurrencySearchFilter): CurrencySearchFilter {
        //copy object into new one to void reference.
        let item = <CurrencySearchFilter>{
            Keyword: filter.Keyword,
            Start: filter.Start,
            Take: filter.Take,
            Active: filter.Active,
            CompanyId: filter.CompanyId,
            UserId: filter.UserId
        };
        return item;
    }
    private getCurrencies(filter: CurrencySearchFilter) {
        if (this.isWating)
            return;
        this.currencySub = this.currencyService
            .get(filter).finally(() => this.isWating = false)
            .subscribe(response => {
                if (response.Data != null) {
                    let result = [...this.currencies, ...response.Data];
                    this.currencies = result;
                    this.hasNextPage = (response.Data.length <= 0 || this.currencies.length < this.filter.Take) ? false : true;
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
        this.currencies = [];
        this.filter = <CurrencySearchFilter>{ Keyword: '*', Take: 100, Start: 0, Active: null, CompanyId: null, UserId: null }
        this.status = 0;
    }
}
