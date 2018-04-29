import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { Helper } from '../../core/helper';
import { RackModel } from '../shared/rack-model';
import { RackService } from '../shared/rack.service';
import { SpinnerService } from '../../core/spinner/spinner.service';
import { ModalService, ModalType } from '../../core/modal/modal.service';
import { ToastService } from '../../core/toast/toast.service';
import { ActivatedRoute, Router } from '@angular/router';
import { RackSearchFilter } from '../shared/rack-search-filter'
import {Location} from '@angular/common';
@Component({
    selector: 'app-rack-list',
    templateUrl: './rack-list.component.html'
})
export class RackListComponent implements OnInit, OnDestroy {
    isWating: boolean = false;
    private rackSub: Subscription;
    racks: RackModel[] = [];
    filter: RackSearchFilter = <RackSearchFilter>{ Keyword: '*', Take: 100, Start: 0, RoomId: null, CompanyId: null, UserId: null };
    hasNextPage: boolean = true;
    constructor(   private route: ActivatedRoute,private rackService: RackService, private spinnerService: SpinnerService, private toastService: ToastService,
        private modalService: ModalService,private locationService: Location, private router: Router, private activatedRoute: ActivatedRoute) { }

    ngOnInit() {
        this.racks = [];
        this.route
            .queryParams
            .map(params => params['roomId'])
            .do(roomId => this.filter.RoomId = roomId)
            .subscribe(id => this.getRacks(this.copyFilter(this.filter)));

    }
    onSearch(event: string) {
        this.filter.Keyword = event.length > 0 ? event : '*';
        this.filter.Start = 0;
        this.racks = [];
        this.getRacks(this.copyFilter(this.filter));
    }
    
    loadMore() {
        this.filter.Start = this.racks.length;
        this.getRacks(this.copyFilter(this.filter));
    }
    onAdd() {
        let rack = new RackModel();
        rack.Id = Helper.GUID.NewID().replace(/-/g, '');
        rack.RoomId=this.filter.RoomId;
        this.rackService.show(rack, false).then(x => {
            if (x != null) {
                {
                    this.reset();
                    this.getRacks(this.copyFilter(this.filter));
                }
            }
        });
    }
    onEdit(rack: RackModel) {
        //copy object into new one to void reference.
        let item = <RackModel>{
            Id: rack.Id,
            Name: rack.Name,
            Description: rack.Description,
            RoomId: rack.RoomId,
            Status: rack.Status,
            Rows:rack.Rows,
            Columns:rack.Columns,
            SecondaryStatus:rack.SecondaryStatus,
            Index:rack.Index,
            CompanyId:rack.CompanyId,
            BoxCapacity:rack.BoxCapacity,
            CreatedBy:rack.CreatedBy,
            CreatedOn:rack.CreatedOn
        };

        this.rackService.show(item, true).then(e => {
            if (e != null) {
                let index = this.racks.findIndex(x => x.Id == e.Id);
                if (index >= 0) {
                    this.racks.splice(index, 1, e);
                }
            }

        });
    }
    onDelete(rack: RackModel) {
        if (this.isWating)
            return;
        this.modalService.activate('Are you sure you want to delete this?').then(e => {

            if (e == true && rack != null)
                this.delete(rack);
        });
    }
    onView(rack: RackModel) {
        this.router.navigate(['rack-boxes'],{ queryParams: { rackId: rack.Id } });
    }
    onBack()
    {
        this.locationService.back();
    }
    ngOnDestroy() {
        if (this.rackSub != undefined && this.rackSub != null)
            this.rackSub.unsubscribe();
    }
    private delete(rack: RackModel) {
        this.isWating = true;
        this.rackSub = this.rackService.delete(rack.Id).subscribe(result => {
            this.isWating = false;
            this.spinnerService.hide();
            if (result.IsSucceed) {
                let index = this.racks.indexOf(rack);
                if (index != null && index >= 0)
                    this.racks.splice(index, 1);
            }
        },
            error => {
                this.isWating = false;
                this.spinnerService.hide();
                this.errorAction(error);
            });
        this.spinnerService.show();
    }
    private copyFilter(filter: RackSearchFilter): RackSearchFilter {
        //copy object into new one to void reference.
        let item = <RackSearchFilter>{
            Keyword: filter.Keyword,
            Start: filter.Start,
            Take: filter.Take,
            CompanyId: filter.CompanyId,
            UserId: filter.UserId,
            RoomId:filter.RoomId
        };
        return item;
    }
    private getRacks(filter: RackSearchFilter) {
        if (this.isWating)
            return;
        console.log(filter);
        this.rackSub = this.rackService
            .get(filter).finally(() => this.isWating = false)
            .subscribe(response => {
                if (response.Data != null) {
                    let result = [...this.racks, ...response.Data];
                    this.racks = result;
                    this.hasNextPage = (response.Data.length <= 0 || this.racks.length < this.filter.Take) ? false : true;
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
        this.racks = [];
        let roomId=this.filter.RoomId;
        this.filter = <RackSearchFilter>{ Keyword: '*', Take: 100, Start: 0, RoomId: roomId, CompanyId: null, UserId: null };
    }
}
