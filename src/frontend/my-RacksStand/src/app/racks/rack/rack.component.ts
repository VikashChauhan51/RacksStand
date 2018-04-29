import { Component,ViewChild, Input, OnDestroy, OnInit, HostListener } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, ReactiveFormsModule, NgForm } from '@angular/forms'
import { RackService } from '../shared/rack.service';
import { RackModel } from '../shared/rack-model';
import { Helper } from '../../core/helper';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../core/toast/toast.service';
import { Subscription } from 'rxjs/Subscription';
const KEY_ESC = 27;

@Component({
    selector: 'app-rack',
    templateUrl: './rack.component.html'
})
export class RackComponent implements OnInit, OnDestroy {
    @Input() rack: RackModel = <RackModel>{};
    isWating: boolean = false;
    negativeOnClick: (e: RackModel) => void;
    positiveOnClick: (e: RackModel) => void;
    private rackSub: Subscription;
    isEdit: boolean = false;
    modalElement: any;
    @ViewChild('rackForm') public rackForm: NgForm;
    constructor(private rackService: RackService, private router: Router, private activatedRoute: ActivatedRoute, private toastService: ToastService) {
        rackService.show = this.activate.bind(this);
    }
    activate(rack: RackModel, isEdit: boolean) {
        let promise = new Promise<RackModel>((resolve, reject) => {
            this.negativeOnClick = (e: RackModel) => resolve(e);
            this.positiveOnClick = (e: RackModel) => resolve(e);
            this.isEdit = isEdit;
            this.rack = rack;
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
        let _rack = JSON.parse(JSON.stringify(this.rack));
        if (!this.isEdit)
            this.add(_rack);
        else
            this.update(_rack);
     

    }
    private add(rack: RackModel) {
        this.isWating = true;
        this.rackSub = this.rackService.save(rack).finally(() => this.reset()).subscribe(result => {
            if (result.IsSucceed)
                this.positiveOnClick(rack);
            else
                this.negativeOnClick(null);

        },
            error => {

                this.negativeOnClick(null);
                this.errorAction(error);
            });

    }
    private update(rack: RackModel) {
        this.isWating = true;
        this.rackSub = this.rackService.update(rack).finally(() => this.reset()).subscribe(result => {
            if (result.IsSucceed)
                this.positiveOnClick(rack);
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
        this.rack = <RackModel>{};
        if (this.rackSub != undefined && this.rackSub!=null)
            this.rackSub.unsubscribe();
        this.rackForm.reset();
    }
    private show() {
        this.modalElement.style.display = "block";
    }
    ngOnInit() {
        this.modalElement = document.getElementById('rack');
        this.rack = <RackModel>{};
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
