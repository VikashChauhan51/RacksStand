import { Component, Input, OnDestroy, OnInit, HostListener } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, ReactiveFormsModule, NgForm } from '@angular/forms'
import { RackBoxService } from '../shared/rack-box.service';
import { RackBoxModel } from '../shared/rack-box-model';
import { Helper } from '../../core/helper';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../core/toast/toast.service';
import { Subscription } from 'rxjs/Subscription';
const KEY_ESC = 27;

@Component({
    selector: 'app-rack-box',
    templateUrl: './rack-box.component.html'
})
export class RackBoxComponent implements OnInit, OnDestroy {
    @Input() rackBox: RackBoxModel = <RackBoxModel>{};
    isWating: boolean = false;
    negativeOnClick: (e: RackBoxModel) => void;
    positiveOnClick: (e: RackBoxModel) => void;
    private rackSub: Subscription;
    modalElement: any;
    private tempForm:NgForm=null;
    constructor(private rackBoxService: RackBoxService, private router: Router, private activatedRoute: ActivatedRoute, private toastService: ToastService) {
        rackBoxService.show = this.activate.bind(this);
    }
    activate(rackBox: RackBoxModel) {
        let promise = new Promise<RackBoxModel>((resolve, reject) => {
            this.negativeOnClick = (e: RackBoxModel) => resolve(e);
            this.positiveOnClick = (e: RackBoxModel) => resolve(e);
            this.rackBox = rackBox;
            this.show();
        });

        return promise;
    }
    save(form: NgForm) {
        this.tempForm=form;
        let isValid = form.valid;
        if (!isValid || this.isWating) {
            Object.keys(form.controls).forEach(key => {
                (form.controls[key] as FormControl).markAsTouched();
            });
            return;
        }


        //copy JavaScript object to new variable NOT by reference.
        let _rackBox = JSON.parse(JSON.stringify(this.rackBox));
        this.update(_rackBox);
     

    }
    
    private update(rackBox: RackBoxModel) {
        this.isWating = true;
        this.rackSub = this.rackBoxService.update(rackBox).finally(() => this.reset()).subscribe(result => {
            if (result.IsSucceed)
                this.positiveOnClick(rackBox);
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
        this.rackBox = <RackBoxModel>{};
        if (this.rackSub != undefined && this.rackSub!=null)
            this.rackSub.unsubscribe();
        if(this.tempForm!=null){
            this.tempForm.reset();
            this.tempForm=null;
        }
    }
    private show() {
        this.modalElement.style.display = "block";
    }
    ngOnInit() {
        this.modalElement = document.getElementById('rackBox');
        this.rackBox = <RackBoxModel>{};
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
