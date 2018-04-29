import { Component, Output, Input, EventEmitter, } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, ReactiveFormsModule } from '@angular/forms'
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/throttleTime';
import 'rxjs/add/observable/fromEvent';

@Component({
    selector: 'search-box',
    template: `<div class='input-group'  >
  <input type='search' class='form-control' [formControl]='searchBoxControl' 
   [placeholder]='placeholder' />
 
  </div> `
})
export class SearchBox {
    @Input() placeholder: string = 'Search';
    searchBoxControl = new FormControl();
    @Output() searchEvent: EventEmitter<string> = new EventEmitter<string>();

    constructor() {
        this.searchBoxControl.valueChanges.debounceTime(500)
            .subscribe((event) => this.searchEvent.emit(event));
            
    }
}