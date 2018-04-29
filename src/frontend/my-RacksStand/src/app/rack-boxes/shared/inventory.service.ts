import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Helper } from '../../core/helper';
import { ResponseMessage } from '../../core/response-message';
import { InventoryModel } from './rack-box-model';
import { SessionService } from '../../core/session.service';
import { InventorySearchFilter } from '../shared/rack-box-search-filter'
@Injectable()
export class InventoryService {

    constructor(private http: Http, private sessionService: SessionService) { }
    show: (inventory: InventoryModel, isEdit: boolean) => Promise<InventoryModel>;
    save(inventory: InventoryModel): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Inventory/Add`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.post(url, JSON.stringify(inventory), options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
    update(inventory: InventoryModel): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Inventory/Update`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.post(url, JSON.stringify(inventory), options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
   
    get(filter: InventorySearchFilter): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Inventory/Get`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.post(url, JSON.stringify(filter), options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
    getInventory(id: string): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Inventory/GetRack?id=${id}`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.get(url, options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
    delete(id: string): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Inventory/Delete?id=${id}`;
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
