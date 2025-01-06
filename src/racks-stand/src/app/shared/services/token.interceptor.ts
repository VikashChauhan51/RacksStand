import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root'
})
export class TokenInterceptor implements HttpInterceptor {
  constructor(private tokenService: TokenService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let clonedRequest = req;

    // Add the authorization header with the access token
    clonedRequest = clonedRequest.clone({
      setHeaders: {
        Authorization: `Bearer ${this.tokenService.getAccessToken()}`
      }
    });

    return next.handle(clonedRequest).pipe(
      catchError((error: HttpErrorResponse) => {
        // If we get a 401 error, try refreshing the token
        if (error.status === 401) {
          return this.tokenService.refreshAccessToken().pipe(
            switchMap(() => {
              // Retry the failed request with the new access token
              clonedRequest = clonedRequest.clone({
                setHeaders: {
                  Authorization: `Bearer ${this.tokenService.getAccessToken()}`
                }
              });
              return next.handle(clonedRequest);
            })
          );
        }
        return throwError(error);
      })
    );
  }
}
