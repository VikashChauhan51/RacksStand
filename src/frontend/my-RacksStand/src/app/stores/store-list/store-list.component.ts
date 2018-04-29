import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { Helper } from '../../core/helper';
import { StoreModel } from '../shared/store-model';
import { StoreService } from '../shared/store.service';
import { SpinnerService } from '../../core/spinner/spinner.service';
import { ModalService, ModalType } from '../../core/modal/modal.service';
import { ToastService } from '../../core/toast/toast.service';
import { ActivatedRoute, Router } from '@angular/router';
import { StoreSearchFilter } from '../shared/store-search-filter'
@Component({
    selector: 'app-store-list',
    templateUrl: './store-list.component.html'
})
export class StoreListComponent implements OnInit, OnDestroy {
    isWating: boolean = false;
    private storeSub: Subscription;
    stores: StoreModel[] = [];
    filter: StoreSearchFilter = <StoreSearchFilter>{ Keyword: '*', Take: 100, Start: 0, CompanyId: null, UserId: null }
    hasNextPage: boolean = true;
    
    constructor(private storeService: StoreService, private spinnerService: SpinnerService, private toastService: ToastService,
        private modalService: ModalService, private router: Router, private activatedRoute: ActivatedRoute) { }

    ngOnInit() {
        this.stores = [];
        this.getstores(this.copyFilter(this.filter));
    }
    onSearch(event: string) {
        this.filter.Keyword = event.length > 0 ? event : '*';
        this.filter.Start = 0;
        this.stores = [];
        this.getstores(this.copyFilter(this.filter));
    }

    loadMore() {
        this.filter.Start = this.stores.length;
        this.getstores(this.copyFilter(this.filter));
    }
    onAdd() {
        let store = new StoreModel();
        store.Id = Helper.GUID.NewID().replace(/-/g, '');
        store.Country = 0;
        store.Rooms = [];
        this.storeService.show(store, false).then(x => {
            if (x != null) {
                {
                    this.reset();
                    this.getstores(this.copyFilter(this.filter));
                }
            }
        });
    }
    onEdit(store: StoreModel) {
        //copy object into new one to void reference.
        let item = <StoreModel>{
            Id: store.Id,
            Name: store.Name,
            CompanyId: store.CompanyId,
            City: store.City,
            Country: store.Country,
            Email: store.Email,
            Fax: store.Fax,
            Phone: store.Phone,
            Description: store.Description,
            State: store.State,
            Status: store.Status,
            Street: store.Street,
            ZipCode: store.ZipCode,
            SecondaryStatus:store.SecondaryStatus,
            Index:store.Index

        };

        this.storeService.show(item, true).then(e => {
            if (e != null) {
                let index = this.stores.findIndex(x => x.Id == e.Id);
                if (index >= 0) {
                    this.stores.splice(index, 1, e);
                }
            }

        });
    }
    onDelete(store: StoreModel) {
        if (this.isWating)
            return;
        this.modalService.activate('Are you sure you want to delete this?').then(e => {

            if (e == true && store != null)
                this.delete(store);
        });
    }
    onView(store: StoreModel) {
        this.router.navigate(['rooms'],{ queryParams: { storeId: store.Id } });
    }
    ngOnDestroy() {
        if (this.storeSub != undefined && this.storeSub != null)
            this.storeSub.unsubscribe();
    }
    private delete(store: StoreModel) {
        this.isWating = true;
        this.storeSub = this.storeService.delete(store.Id).subscribe(result => {
            this.isWating = false;
            this.spinnerService.hide();
            if (result.IsSucceed) {
                let index = this.stores.indexOf(store);
                if (index != null && index >= 0)
                    this.stores.splice(index, 1);
            }
        },
            error => {
                this.isWating = false;
                this.spinnerService.hide();
                this.errorAction(error);
            });
        this.spinnerService.show();
    }
    private copyFilter(filter: StoreSearchFilter): StoreSearchFilter {
        //copy object into new one to void reference.
        let item = <StoreSearchFilter>{
            Keyword: filter.Keyword,
            Start: filter.Start,
            Take: filter.Take,
            CompanyId: filter.CompanyId,
            UserId: filter.UserId
        };
        return item;
    }
    private getstores(filter: StoreSearchFilter) {
        if (this.isWating)
            return;
       
        this.storeSub = this.storeService
            .get(filter).finally(() => this.isWating = false)
            .subscribe(response => {
                if (response.Data != null) {
                    let result = [...this.stores, ...response.Data];
                    this.stores = result;
                    this.hasNextPage = (response.Data.length <= 0 || this.stores.length < this.filter.Take) ? false : true;
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
        this.stores = [];
        this.filter = <StoreSearchFilter>{ Keyword: '*', Take: 100, Start: 0, CompanyId: null, UserId: null }
    }
}
