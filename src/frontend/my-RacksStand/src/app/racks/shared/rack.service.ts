import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Helper } from '../../core/helper';
import { ResponseMessage } from '../../core/response-message';
import { RackModel } from './rack-model';
import { SessionService } from '../../core/session.service';
import { RackSearchFilter } from '../shared/rack-search-filter'
@Injectable()
export class RackService {

    constructor(private http: Http, private sessionService: SessionService) { }
    show: (rack: RackModel, isEdit: boolean) => Promise<RackModel>;
    save(rack: RackModel): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Rack/Add`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.post(url, JSON.stringify(rack), options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
    update(rack: RackModel): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Rack/Update`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.post(url, JSON.stringify(rack), options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
    get(filter: RackSearchFilter): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Rack/Get`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.post(url, JSON.stringify(filter), options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }

    getRack(id: string): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Rack/GetRack?id=${id}`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.get(url, options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
    delete(id: string): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Rack/Delete?id=${id}`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.get(url, options)
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
