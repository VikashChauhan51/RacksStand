import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { Helper } from '../../core/helper';
import { RoomModel,RackModel } from '../shared/room-model';
import { RoomService } from '../shared/room.service';
import { SpinnerService } from '../../core/spinner/spinner.service';
import { ModalService, ModalType } from '../../core/modal/modal.service';
import { ToastService } from '../../core/toast/toast.service';
import { ActivatedRoute, Router } from '@angular/router';
import { RoomSearchFilter } from '../shared/room-search-filter'
import {Location} from '@angular/common';
@Component({
    selector: 'app-room-list',
    templateUrl: './room-list.component.html'
})
export class RoomListComponent implements OnInit, OnDestroy {
    isWating: boolean = false;
    private roomSub: Subscription;
    rooms: RoomModel[] = [];
    filter: RoomSearchFilter = <RoomSearchFilter>{ Keyword: '*', Take: 100, Start: 0, StoreId: null, CompanyId: null, UserId: null }
    hasNextPage: boolean = true;
    constructor(   private route: ActivatedRoute,private roomService: RoomService, private spinnerService: SpinnerService, private toastService: ToastService,
        private modalService: ModalService,private locationService: Location, private router: Router, private activatedRoute: ActivatedRoute) { }

    ngOnInit() {
        this.rooms = [];
        this.route
            .queryParams
            .map(params => params['storeId'])
            .do(storeId => this.filter.StoreId = storeId)
            .subscribe(id => this.getRooms(this.copyFilter(this.filter)));

    }
    onSearch(event: string) {
        this.filter.Keyword = event.length > 0 ? event : '*';
        this.filter.Start = 0;
        this.rooms = [];
        this.getRooms(this.copyFilter(this.filter));
    }
    onBack()
    {
        this.locationService.back();
    }
    loadMore() {
        this.filter.Start = this.rooms.length;
        this.getRooms(this.copyFilter(this.filter));
    }
    onAdd() {
        let room = new RoomModel();
        room.Id = Helper.GUID.NewID().replace(/-/g, '');
        room.StoreId=this.filter.StoreId;
        this.roomService.show(room, false).then(x => {
            if (x != null) {
                {
                    this.reset();
                    this.getRooms(this.copyFilter(this.filter));
                }
            }
        });
    }
    onEdit(room: RoomModel) {
        //copy object into new one to void reference.
        let item = <RoomModel>{
            Id: room.Id,
            Name: room.Name,
            Description: room.Description,
            StoreId: room.StoreId,
            Status: room.Status,
            SecondaryStatus:room.SecondaryStatus,
            CompanyId:room.CompanyId,
            Index:room.Index,
            CreatedBy:room.CreatedBy,
            CreatedOn:room.CreatedOn
        };

        this.roomService.show(item, true).then(e => {
            if (e != null) {
                let index = this.rooms.findIndex(x => x.Id == e.Id);
                if (index >= 0) {
                    this.rooms.splice(index, 1, e);
                }
            }

        });
    }
    onDelete(room: RoomModel) {
        if (this.isWating)
            return;
        this.modalService.activate('Are you sure you want to delete this?').then(e => {

            if (e == true && room != null)
                this.delete(room);
        });
    }
    onView(room: RoomModel) {
        this.router.navigate(['racks'],{ queryParams: { roomId: room.Id } });
    }
    ngOnDestroy() {
        if (this.roomSub != undefined && this.roomSub != null)
            this.roomSub.unsubscribe();
    }
    private delete(room: RoomModel) {
        this.isWating = true;
        this.roomSub = this.roomService.delete(room.Id).subscribe(result => {
            this.isWating = false;
            this.spinnerService.hide();
            if (result.IsSucceed) {
                let index = this.rooms.indexOf(room);
                if (index != null && index >= 0)
                    this.rooms.splice(index, 1);
            }
        },
            error => {
                this.isWating = false;
                this.spinnerService.hide();
                this.errorAction(error);
            });
        this.spinnerService.show();
    }
    private copyFilter(filter: RoomSearchFilter): RoomSearchFilter {
        //copy object into new one to void reference.
        let item = <RoomSearchFilter>{
            Keyword: filter.Keyword,
            Start: filter.Start,
            Take: filter.Take,
            CompanyId: filter.CompanyId,
            UserId: filter.UserId,
            StoreId:filter.StoreId
        };
        return item;
    }
    private getRooms(filter: RoomSearchFilter) {
        if (this.isWating)
            return;
        this.roomSub = this.roomService
            .get(filter).finally(() => this.isWating = false)
            .subscribe(response => {
                if (response.Data != null) {
                    let result = [...this.rooms, ...response.Data];
                    this.rooms = result;
                    this.hasNextPage = (response.Data.length <= 0 || this.rooms.length < this.filter.Take) ? false : true;
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
        this.rooms = [];
        let storeId=this.filter.StoreId;
        this.filter = <RoomSearchFilter>{ Keyword: '*', Take: 100, Start: 0, StoreId: storeId, CompanyId: null, UserId: null };

    }
}
