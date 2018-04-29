import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Helper } from '../../core/helper';
import { ResponseMessage } from '../../core/response-message';
import { SupplierModel,SupplierAddressModel } from './supplier-model';
import { SessionService } from '../../core/session.service';
import { SupplierSearchFilter } from '../shared/supplier-search-filter';
@Injectable()
export class SupplierService {

    constructor(private http: Http, private sessionService: SessionService)
    { }
    show: (supplier: SupplierModel, isEdit: boolean) => Promise<SupplierModel>;
    activate: (address: SupplierAddressModel, isEdit: boolean) => Promise<SupplierAddressModel>;
    save(supplier: SupplierModel): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Supplier/Add`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.post(url, JSON.stringify(supplier), options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
    update(supplier: SupplierModel): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Supplier/Update`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.post(url, JSON.stringify(supplier), options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
    get(filter: SupplierSearchFilter): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Supplier/Get`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.post(url, JSON.stringify(filter), options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
    getSupplier(id: string): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Supplier/GetSupplier?id=${id}`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.get(url, options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
    delete(id:string): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Supplier/Delete?id=${id}`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.get(url, options)
              .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
    private createHeader(): any {
        var headers = new Headers();
        // headers.append('Content-Type', 'application/x-www-form-urlencoded');
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
