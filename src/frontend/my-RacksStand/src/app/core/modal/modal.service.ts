import { Injectable, Optional, SkipSelf } from '@angular/core';
export enum ModalType {
    Success,
    Info,
    Warning,
    Danger,
    Confirm
}

@Injectable()
export class ModalService {
    constructor( @Optional() @SkipSelf() prior: ModalService) {
        if (prior) return prior;
    }
    activate: (message?: string, title?: string,mode?:ModalType) => Promise<boolean>;
}
