import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Helper } from '../../core/helper';
import { ResponseMessage } from '../../core/response-message';
import { RackBoxModel } from './rack-box-model';
import { SessionService } from '../../core/session.service';
import { RackBoxSearchFilter } from '../shared/rack-box-search-filter'
@Injectable()
export class RackBoxService {

    constructor(private http: Http, private sessionService: SessionService) { }
    show: (rackBox: RackBoxModel) => Promise<RackBoxModel>;
    update(rackBox: RackBoxModel): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}RackBox/Update`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.post(url, JSON.stringify(rackBox), options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
   
    get(filter: RackBoxSearchFilter): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}RackBox/Get`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.post(url, JSON.stringify(filter), options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
  
    private createHeader(): any {
        var headers = new Headers();
        headers.append('Authorization', `${this.getSessionId()}`);
        headers.append('Content-Type', 'application/json; charset=UTF-8');
        return headers;
    }
    private getSessionId(): string {
        let res = JSON.parse(this.sessionService.getSession());
        return res.SessionId;

    }
    private extractData<T>(res: Response) {
        let body = res.json();
        return <T>(body && body || {});
    }
    private handleError(error: Response | any) {
        let errMsg: {};
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = { status: error.status, message: error.statusText || err };
        } else {
            errMsg = { status: error.status, message: error.message || error.toString() };
        }
        return Observable.throw(errMsg);
    }
}
