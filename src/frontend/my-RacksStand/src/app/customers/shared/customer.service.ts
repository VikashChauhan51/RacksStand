import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Helper } from '../../core/helper';
import { ResponseMessage } from '../../core/response-message';
import { CustomerModel ,CustomerAddressModel} from './customer-model';
import { SessionService } from '../../core/session.service';
import { CustomerSearchFilter } from '../shared/customer-search-filter';
@Injectable()
export class CustomerService {

    constructor(private http: Http, private sessionService: SessionService)
    { }
    show: (customer: CustomerModel, isEdit: boolean) => Promise<CustomerModel>;
    activate: (address: CustomerAddressModel, isEdit: boolean) => Promise<CustomerAddressModel>;
    save(customer: CustomerModel): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Customer/Add`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.post(url, JSON.stringify(customer), options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
    update(customer: CustomerModel): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Customer/Update`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.post(url, JSON.stringify(customer), options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
    get(filter: CustomerSearchFilter): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Customer/Get`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.post(url, JSON.stringify(filter), options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
    getCustomer(id: string): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Customer/GetCustomer?id=${id}`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.get(url, options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
    delete(id:string): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Customer/Delete?id=${id}`;
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
