import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs/Observable';


export interface CanComponentDeactivate {
    canDeactivate: () => any;
}


@Injectable()
export class CanDeactivateGuardService implements  CanDeactivate<CanComponentDeactivate> {
  canDeactivate(component: CanComponentDeactivate): Observable<boolean> | boolean {
    // run the function for canDeactivate and if its a promise or a boolean we handle it either way
    return component.canDeactivate ? this.toObservable(component.canDeactivate()) : true;
  }

  private toObservable(deactivate: Promise<boolean> | boolean): Observable<boolean> | boolean {
      let p = Promise.resolve(deactivate);
      let o = Observable.fromPromise(p);
      return o;
  }
}
