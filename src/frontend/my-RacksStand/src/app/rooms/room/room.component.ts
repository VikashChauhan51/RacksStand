import { Component,ViewChild, Input, OnDestroy, OnInit, HostListener } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, ReactiveFormsModule, NgForm } from '@angular/forms'
import { RoomService } from '../shared/room.service';
import { RoomModel } from '../shared/room-model';
import { Helper } from '../../core/helper';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../core/toast/toast.service';
import { Subscription } from 'rxjs/Subscription';
const KEY_ESC = 27;

@Component({
    selector: 'app-room',
    templateUrl: './room.component.html'
})
export class RoomComponent implements OnInit, OnDestroy {
    @Input() room: RoomModel = <RoomModel>{};
    isWating: boolean = false;
    negativeOnClick: (e: RoomModel) => void;
    positiveOnClick: (e: RoomModel) => void;
    private roomSub: Subscription;
    isEdit: boolean = false;
    modalElement: any;
    @ViewChild('roomForm') public roomForm: NgForm;
    constructor(private roomService: RoomService, private router: Router, private activatedRoute: ActivatedRoute, private toastService: ToastService) {
        roomService.show = this.activate.bind(this);
    }
    activate(room: RoomModel, isEdit: boolean) {
        let promise = new Promise<RoomModel>((resolve, reject) => {
            this.negativeOnClick = (e: RoomModel) => resolve(e);
            this.positiveOnClick = (e: RoomModel) => resolve(e);
            this.isEdit = isEdit;
            this.room = room;
            this.show();
        });

        return promise;
    }
    save(form: NgForm) {
        let isValid = form.valid;
        if (!isValid || this.isWating) {
            Object.keys(form.controls).forEach(key => {
                (form.controls[key] as FormControl).markAsTouched();
            });
            return;
        }


        //copy JavaScript object to new variable NOT by reference.
        let _room = JSON.parse(JSON.stringify(this.room));
        if (!this.isEdit)
            this.add(_room);
        else
            this.update(_room);
     

    }
    private add(room: RoomModel) {
        this.isWating = true;
        this.roomSub = this.roomService.save(room).finally(() => this.reset()).subscribe(result => {
            if (result.IsSucceed)
                this.positiveOnClick(room);
            else
                this.negativeOnClick(null);

        },
            error => {

                this.negativeOnClick(null);
                this.errorAction(error);
            });

    }
    private update(room: RoomModel) {
        this.isWating = true;
        this.roomSub = this.roomService.update(room).finally(() => this.reset()).subscribe(result => {
            if (result.IsSucceed)
                this.positiveOnClick(room);
            else
                this.negativeOnClick(null);

        },
            error => {
                this.negativeOnClick(null);
                this.errorAction(error);
            });
    }
    cancel() {
        if (this.isWating)
            return;
        this.negativeOnClick(null);
        this.reset();
    }
    private reset() {
        this.modalElement.style.display = "none";
        this.isWating = false;
        this.isEdit = false;
        this.room = <RoomModel>{};
        if (this.roomSub != undefined && this.roomSub!=null)
            this.roomSub.unsubscribe();
            this.roomForm.reset();
    }
    private show() {
        this.modalElement.style.display = "block";
    }
    ngOnInit() {
        this.modalElement = document.getElementById('room');
        this.room = <RoomModel>{};
    }
    ngOnDestroy() {
        this.reset();
    }

    private errorAction(error: any) {
        if (error.status == 0)
            this.router.navigate(['error']);
        else
            this.toastService.activate(error.message);
    }
}
