import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddressComponent } from './address.component';
import { AddressService } from './address.service';
import { FormsModule }   from '@angular/forms';

@NgModule({
    imports: [CommonModule, FormsModule],
    exports: [AddressComponent],
    declarations: [AddressComponent],
    providers: [AddressService]
})
export class AddressModule {
    constructor() {
        
    }
}
