import { Injectable, Optional, SkipSelf } from '@angular/core';

export interface AddressModel {
    Id: string,
    City: string,
    Country: number,
    Email: string,
    Fax: string,
    Phone: string,
    Remark: string,
    State: string,
    Status: number,
    Street: string,
    ZipCode: string,
}
@Injectable()
export class AddressService {

    constructor( ) {
    }
    show: (address: AddressModel, isEdit: boolean) => Promise<AddressModel>;

}
