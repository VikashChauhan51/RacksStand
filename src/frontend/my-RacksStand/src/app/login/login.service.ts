import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { LoginModel } from './shared/login-model';
import { Helper } from '../core/helper';
import { ResponseMessage } from '../core/response-message';
@Injectable()
export class LoginService {

      
    constructor(private http: Http) { }
    login(login: LoginModel): Observable<ResponseMessage> {
        let loginUrl: string = `${Helper.AppConfig.BASE_URL}Account/Login`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.post(loginUrl, JSON.stringify(login) , options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }

    logout() {
        // ToDO
    }
    forgot(email: string): Observable<ResponseMessage> {

        let forgotUrl: string = `${Helper.AppConfig.BASE_URL}Account/Forgot?email=${email}`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.get(forgotUrl, options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);

    }
    reset(password: string, token: string): Observable<ResponseMessage> {
        let resetUrl: string = `${Helper.AppConfig.BASE_URL}Account/Reset?token=${token}&password=${password}`;
        let headers = this.createHeader();
        let options = new RequestOptions({ headers: headers });
        return this.http.get(resetUrl, options)
            .map(res => this.extractData<ResponseMessage>(res)).catch(this.handleError);
    }
    private createHeader(): any {
        var headers = new Headers();
        // headers.append('Content-Type', 'application/x-www-form-urlencoded');
        headers.append('Authorization', '');
        headers.append('Content-Type', 'application/json; charset=UTF-8');
        return headers;
    }

    private extractData<T>(res: Response) {
               let body = res.json();
        return <T>(body && body || {});
    }
    private handleError(error: Response | any) {
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        return Observable.throw(errMsg);
    }
}
