import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Helper } from './helper';
import { ResponseMessage } from './response-message';

@Injectable()
export class SessionService {
   
    constructor(private http: Http) { }
    public isLoggedIn(): boolean
    {
        let isLogin = true;
        let value = localStorage.getItem(Helper.AppConfig.SESSION_KEY);
        if (value == undefined || value == null || value=='' || value.length == 0) 
            isLogin = false;
        return isLogin ;
       
    }
    public setSession(obj: string) {
        localStorage.setItem(Helper.AppConfig.SESSION_KEY, obj);
    }
    public getSession(): string {
        return localStorage.getItem(Helper.AppConfig.SESSION_KEY);
     
    }
    public clearSession() {
        localStorage.removeItem(Helper.AppConfig.SESSION_KEY);
        
    }
logout(): Observable<ResponseMessage> 
{
     
    let url: string = `${Helper.AppConfig.BASE_URL}Account/Logout`;
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
      let res = JSON.parse(this.getSession());
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
