import { Component, Input,OnDestroy, OnInit, HostListener } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, ReactiveFormsModule,NgForm } from '@angular/forms'
import { TaxService } from '../shared/tax.service';
import { TaxModel } from '../shared/tax-model';
import {Helper} from '../../core/helper';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../core/toast/toast.service';
import { Subscription } from 'rxjs/Subscription';
const KEY_ESC = 27;

@Component({
  selector: 'app-tax',
  templateUrl: './tax.component.html'
})
export class TaxComponent implements OnInit ,OnDestroy{
 @Input() tax: TaxModel = <TaxModel>{};
    isWating: boolean = false;
    negativeOnClick: (e: TaxModel) => void;
    positiveOnClick: (e: TaxModel) => void;
     private taxSub: Subscription;
     isEdit:boolean=false;
    modalElement:any;
    private tempForm:NgForm=null;
  constructor(private taxService:TaxService,private router: Router, private activatedRoute: ActivatedRoute, private toastService:ToastService) {
     taxService.show = this.activate.bind(this);
   }
activate(tax: TaxModel, isEdit: boolean) {
        let promise = new Promise<TaxModel>((resolve, reject) => {
            this.negativeOnClick = (e: TaxModel) => resolve(e);
            this.positiveOnClick = (e: TaxModel) => resolve(e);
            this.isEdit=isEdit;
            this.tax=tax;
             this.show();
        });

        return promise;
    }
    save(form: NgForm) {
        this.tempForm=form;
        let isValid=form.valid;
         if (!isValid || this.isWating) {
            Object.keys(form.controls).forEach(key => {
                (form.controls[key] as FormControl).markAsTouched();
            });
            return;
        }
      
          //copy JavaScript object to new variable NOT by reference.
        let _tax = JSON.parse(JSON.stringify(this.tax));
        if(!this.isEdit)
        this.add(_tax);
        else
        this.update(_tax);
         
       
    }
    private add(tax:TaxModel)
    {
          this.isWating = true;
        this.taxSub=this.taxService.save(tax).finally(()=>this.reset()).subscribe(result=>
        {
            if(result.IsSucceed)
            this.positiveOnClick(tax);
            else
            this.negativeOnClick(null);
            
        },
            error => {
                 
                this.negativeOnClick(null);
               this.errorAction(error);
            });

    }
    private update(tax:TaxModel)
    {
          this.isWating = true;
        this.taxSub=this.taxService.update(tax).finally(()=>this.reset()).subscribe(result=>
        {
             if(result.IsSucceed)
            this.positiveOnClick(tax);
            else
            this.negativeOnClick(null);
            
        },
            error => {
                this.negativeOnClick(null);
               this.errorAction(error);
            });
    }
  cancel() {
      if(this.isWating) 
      return;
        this.negativeOnClick(null);
        this.reset();
    }
    private reset() {
        this.modalElement.style.display = "none";
        this.isWating = false;
        this.isEdit=false;
        this.tax = <TaxModel>{};
       if(this.taxSub!=undefined && this.taxSub!=null)
       this.taxSub.unsubscribe();
        if(this.tempForm!=null){
            this.tempForm.reset();
            this.tempForm=null;
        }
    }
    private show() {
        this.modalElement.style.display = "block";
    }
    ngOnInit() {
        this.modalElement = document.getElementById('tax');
       this.tax = <TaxModel>{};
    }
    ngOnDestroy() {
        this.reset();
    }
//  @HostListener('document:keyup', ['$event'])
//     onKeyUp(ev: KeyboardEvent) {
//         if (ev.keyCode === KEY_ESC) {
//             this.cancel();
//         }
//     }
    private errorAction(error:any)
    {
        if(error.status==0)
        this.router.navigate(['error']);
        else
        this.toastService.activate(error.message);
    }
}
