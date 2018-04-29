import { Injectable, Optional, SkipSelf } from '@angular/core';

export enum ToastType {
    Success,
    Info,
    Warning,
    Danger
     
}
@Injectable()
export class ToastService {
    constructor( @Optional() @SkipSelf() prior: ToastService) {
        if (prior) return prior;
    }
    activate: (message: string,type?:ToastType) => void;
    
}