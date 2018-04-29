import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Helper } from '../../core/helper';
import { ResponseMessage } from '../../core/response-message';
import { TaxModel } from './tax-model';
import { SessionService } from '../../core/session.service';
import { TaxSearchFilter } from '../shared/tax-search-filter'
@Injectable()
export class TaxService {

    constructor(private http: Http, private sessionService: SessionService) {

    }
    show: (tax: TaxModel, isEdit: boolean) => Promise<TaxModel>;

    save(tax: TaxModel): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Tax/Add`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.post(url, JSON.stringify(tax), options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
    update(tax: TaxModel): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Tax/Update`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.post(url, JSON.stringify(tax), options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
    get(filter: TaxSearchFilter): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Tax/Gets`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.post(url, JSON.stringify(filter), options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
    getTax(id: string): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Tax/Get?id=${id}`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.get(url, options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
    delete(id: string): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Tax/Delete?id=${id}`;
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
