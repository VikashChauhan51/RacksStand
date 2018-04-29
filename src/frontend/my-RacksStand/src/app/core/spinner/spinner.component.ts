import { Component, OnDestroy, OnInit,Input } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';

import { SpinnerState, SpinnerService,SpinnerType } from './spinner.service';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
   styleUrls: ['./spinner.component.css']
})
export class SpinnerComponent implements OnDestroy, OnInit {
    visible: boolean = false;
    spinnerType:SpinnerType=SpinnerType.DefaultCircle;
    private spinnerStateChanged: Subscription;

    constructor(private spinnerService: SpinnerService) { }

    ngOnInit() {

        this.spinnerStateChanged = this.spinnerService.spinnerState
            .subscribe((state: SpinnerState) =>
                {
                 this.spinnerType=(state.type!=undefined && state.type!=null)?state.type:SpinnerType.DefaultCircle;
                this.visible = state.show;
                });
    }

    ngOnDestroy() {
        this.spinnerStateChanged.unsubscribe();
    }
}

