import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { Helper } from '../../core/helper';
import { RackBoxModel,InventoryModel } from '../shared/rack-box-model';
import { RackBoxService } from '../shared/rack-box.service';
import { InventoryService } from '../shared/inventory.service';
import { SpinnerService } from '../../core/spinner/spinner.service';
import { ModalService, ModalType } from '../../core/modal/modal.service';
import { ToastService } from '../../core/toast/toast.service';
import { ActivatedRoute, Router } from '@angular/router';
import { RackBoxSearchFilter } from '../shared/rack-box-search-filter'
import {Location} from '@angular/common';
@Component({
    selector: 'app-rack-box-list',
    templateUrl: './rack-box-list.component.html'
})
export class RackBoxListComponent implements OnInit, OnDestroy {
    isWating: boolean = false;
    private rackBoxSub: Subscription;
    rackBoxes: RackBoxModel[] = [];
    filter: RackBoxSearchFilter = <RackBoxSearchFilter>{ Keyword: '*', Take: 100, Start: 0, RackId: null, CompanyId: null, UserId: null }
    hasNextPage: boolean = true;
    constructor(   private route: ActivatedRoute,private rackBoxService: RackBoxService, private spinnerService: SpinnerService, private toastService: ToastService,
        private modalService: ModalService,private locationService: Location, private inventoryService:InventoryService,
         private router: Router, private activatedRoute: ActivatedRoute) { }

    ngOnInit() {
        this.rackBoxes = [];
        this.route
            .queryParams
            .map(params => params['rackId'])
            .do(rackId => this.filter.RackId = rackId)
            .subscribe(id => this.getRackBoxes(this.copyFilter(this.filter)));

    }
    onSearch(event: string) {
        this.filter.Keyword = event.length > 0 ? event : '*';
        this.filter.Start = 0;
        this.rackBoxes = [];
        this.getRackBoxes(this.copyFilter(this.filter));
    }
    
    loadMore() {
        this.filter.Start = this.rackBoxes.length;
        this.getRackBoxes(this.copyFilter(this.filter));
    }
     onAdd()
     {
         let item=new InventoryModel();
         item.Id = Helper.GUID.NewID().replace(/-/g, '');
         this.inventoryService.show(item, false).then(x => {
             if (x != null) {
                 {
                     this.reset();
                    
                 }
             }
         });
     }
    onEdit(rackBox: RackBoxModel) {
        //copy object into new one to void reference.
        let item = <RackBoxModel>{
            Id: rackBox.Id,
            Name: rackBox.Name,
            Description: rackBox.Description,
            RackId: rackBox.RackId,
            CompanyId:rackBox.CompanyId,
            SecondaryStatus:rackBox.SecondaryStatus,
            Index:rackBox.Index,
            CurrentSize:rackBox.CurrentSize
        };

        this.rackBoxService.show(item).then(e => {
            if (e != null) {
                let index = this.rackBoxes.findIndex(x => x.Id == e.Id);
                if (index >= 0) {
                    this.rackBoxes.splice(index, 1, e);
                }
            }

        });
    }
    onBack()
    {
        this.locationService.back();
    }
    ngOnDestroy() {
        if (this.rackBoxSub != undefined && this.rackBoxSub != null)
            this.rackBoxSub.unsubscribe();
    }
    
    private copyFilter(filter: RackBoxSearchFilter): RackBoxSearchFilter {
        //copy object into new one to void reference.
        let item = <RackBoxSearchFilter>{
            Keyword: filter.Keyword,
            Start: filter.Start,
            Take: filter.Take,
            CompanyId: filter.CompanyId,
            UserId: filter.UserId,
            RackId:filter.RackId
        };
        return item;
    }
    private getRackBoxes(filter: RackBoxSearchFilter) {
        if (this.isWating)
            return;
        this.rackBoxSub = this.rackBoxService
            .get(filter).finally(() => this.isWating = false)
            .subscribe(response => {
                if (response.Data != null) {
                    let result = [...this.rackBoxes, ...response.Data];
                    this.rackBoxes = result;
                    this.hasNextPage = (response.Data.length <= 0 || this.rackBoxes.length < this.filter.Take) ? false : true;
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
        this.rackBoxes = [];
        let rackId=this.filter.RackId;
        this.filter = <RackBoxSearchFilter>{ Keyword: '*', Take: 100, Start: 0, RackId: rackId, CompanyId: null, UserId: null }
    }
}
