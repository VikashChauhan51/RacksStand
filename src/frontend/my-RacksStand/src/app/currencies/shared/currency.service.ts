import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Helper } from '../../core/helper';
import { ResponseMessage } from '../../core/response-message';
import { CurrencyModel } from './currency-model';
import { SessionService } from '../../core/session.service';
import { CurrencySearchFilter } from '../shared/currency-search-filter'
@Injectable()
export class CurrencyService {

  constructor(private http: Http,private sessionService:SessionService) { }
show: (currency: CurrencyModel, isEdit: boolean) => Promise<CurrencyModel>;

 save(currency: CurrencyModel): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Currency/Add`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.post(url, JSON.stringify(currency), options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
    update(currency: CurrencyModel): Observable<ResponseMessage> {
        let url: string = `${Helper.AppConfig.BASE_URL}Currency/Update`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.post(url, JSON.stringify(currency), options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
get(filter:CurrencySearchFilter): Observable<ResponseMessage> 
{
    let url: string = `${Helper.AppConfig.BASE_URL}Currency/Gets`;
      let headers = this.createHeader();
      let options = new RequestOptions({ headers: headers });
      return this.http.post(url,JSON.stringify(filter), options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
}
getCurrency(id:string): Observable<ResponseMessage> 
{
    let url: string = `${Helper.AppConfig.BASE_URL}Currency/Get?id=${id}`;
      let headers = this.createHeader();
      let options = new RequestOptions({ headers: headers });
      return this.http.get(url, options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
}
delete(id:string): Observable<ResponseMessage> 
{
    let url: string = `${Helper.AppConfig.BASE_URL}Currency/Delete?id=${id}`;
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
private getSessionId():string {
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
            errMsg = {status:error.status,message:error.statusText || err};
        } else {
            errMsg ={status:error.status,message:error.message || error.toString()};
        }
        return Observable.throw(errMsg);
    }
}
