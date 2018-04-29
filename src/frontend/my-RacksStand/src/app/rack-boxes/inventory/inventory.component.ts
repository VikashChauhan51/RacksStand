import { Component,ViewChild, Input, OnDestroy, OnInit, HostListener } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, ReactiveFormsModule, NgForm } from '@angular/forms'
import { InventoryService } from '../shared/inventory.service';
import { InventoryModel } from '../shared/rack-box-model';
import { Helper } from '../../core/helper';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '../../core/toast/toast.service';
import { Subscription } from 'rxjs/Subscription';
import { Observable } from 'rxjs/Observable';
const KEY_ESC = 27;

@Component({
    selector: 'app-inventory',
    templateUrl: './inventory.component.html'
})
export class InventoryComponent implements OnInit, OnDestroy {
    @Input() inventory: InventoryModel = <InventoryModel>{};
    isWating: boolean = false;
    negativeOnClick: (e: InventoryModel) => void;
    positiveOnClick: (e: InventoryModel) => void;
    private inventorySub: Subscription;
    public query = 'Hungary';
    public filteredList = [];
    public countries = [ "Albania","Andorra","Armenia","Austria","Azerbaijan","Belarus",
                        "Belgium","Bosnia & Herzegovina","Bulgaria","Croatia","Cyprus",
                        "Czech Republic","Denmark","Estonia","Finland","France","Georgia",
                        "Germany","Greece","Hungary","Iceland","Ireland","Italy","Kosovo",
                        "Latvia","Liechtenstein","Lithuania","Luxembourg","Macedonia","Malta",
                        "Moldova","Monaco","Montenegro","Netherlands","Norway","Poland",
                        "Portugal","Romania","Russia","San Marino","Serbia","Slovakia","Slovenia",
                        "Spain","Sweden","Switzerland","Turkey","Ukraine","United Kingdom","Vatican City"];
    modalElement: any;
    isEdit: boolean = false;
    @ViewChild('inventoryForm') public inventoryForm: NgForm;
    constructor(private inventoryService: InventoryService, private router: Router, private activatedRoute: ActivatedRoute, private toastService: ToastService) {
        inventoryService.show = this.activate.bind(this);
    }
    activate(inventory: InventoryModel, isEdit: boolean) {
        let promise = new Promise<InventoryModel>((resolve, reject) => {
            this.negativeOnClick = (e: InventoryModel) => resolve(e);
            this.positiveOnClick = (e: InventoryModel) => resolve(e);
            this.inventory = inventory;
            this.isEdit = isEdit;
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
        let _inventory = JSON.parse(JSON.stringify(this.inventory));
        if (!this.isEdit)
        this.add(_inventory);
    else
        this.update(_inventory);
     

    }
    
    observableSource(keyword: any) {
        console.log('keyword:',keyword);
        let filteredList = this.countries.filter(el => el.indexOf(keyword) !== -1);
        return Observable.of(filteredList);
      }

    private add(inventory: InventoryModel) {
        this.isWating = true;
        this.inventorySub = this.inventoryService.save(inventory).finally(() => this.reset()).subscribe(result => {
            if (result.IsSucceed)
                this.positiveOnClick(inventory);
            else
                this.negativeOnClick(null);

        },
            error => {

                this.negativeOnClick(null);
                this.errorAction(error);
            });

    }
    private update(inventory: InventoryModel) {
        this.isWating = true;
        this.inventorySub = this.inventoryService.update(inventory).finally(() => this.reset()).subscribe(result => {
            if (result.IsSucceed)
                this.positiveOnClick(inventory);
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
        this.inventory = <InventoryModel>{};
        if (this.inventorySub != undefined && this.inventorySub!=null)
            this.inventorySub.unsubscribe();
        this.inventoryForm.reset();
    }
    private show() {
        this.modalElement.style.display = "block";
    }
    ngOnInit() {
        this.modalElement = document.getElementById('inventory');
        this.inventory = <InventoryModel>{};
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
