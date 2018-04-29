import { Injectable, Optional, SkipSelf } from '@angular/core';
import { Subject } from 'rxjs/Subject';

export enum SpinnerType {
    ThreeSquareDots,
    SquareSpinner,
    LineSpinner,
    ChasingDots,
    ThreeBounce,
    Circle,
    FadingCircle,
    CubeGrid,
    FoldingCube,
    DefaultCircle
     
}
export interface SpinnerState {
    show: boolean;
    type?:SpinnerType;
}

@Injectable()
export class SpinnerService {
    private spinnerSubject = new Subject<SpinnerState>();

    spinnerState = this.spinnerSubject.asObservable();

    constructor( @Optional() @SkipSelf() prior: SpinnerService) {
        if (prior) { return prior; }
    
    }

    show(type?:SpinnerType) {
        this.spinnerSubject.next(<SpinnerState>{ show: true,type:type });
    }

    hide() {
        this.spinnerSubject.next(<SpinnerState>{ show: false,type:SpinnerType.DefaultCircle });
    }
}
