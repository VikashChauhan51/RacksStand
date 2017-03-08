import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';


@Injectable()
export class ExceptionService{

  constructor() { }
 catchBadResponse: (errorResponse: any) => Observable<any> = (errorResponse: any) => {
    let res = <Response>errorResponse;
    let err = res.json();
    let emsg = err ?
      (err.error ? err.error : JSON.stringify(err)) :
      (res.statusText || 'unknown error');
    return Observable.of(false);
  };
}
