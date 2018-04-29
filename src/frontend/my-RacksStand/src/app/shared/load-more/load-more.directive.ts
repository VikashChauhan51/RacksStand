import { Component, Output, Input, EventEmitter, } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, ReactiveFormsModule } from '@angular/forms'
@Component({
    selector: 'load-more',
     templateUrl: './load-more.html',
    styleUrls: ['./load-more.css'],
})
export class LoadMore {
 @Input()  canLoad:boolean=true;
 @Input()  loading:boolean=false;
 @Input()  totalCount:number=0;
 @Input()  items:number=0;
 constructor(){
     
 }
}