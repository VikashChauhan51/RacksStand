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
    // Clone the request and add the Authorization header if the access token is available
    const accessToken = this.tokenService.getAccessToken();
    let clonedRequest = req;

    if (accessToken) {
      clonedRequest = req.clone({
        setHeaders: {
          Authorization: `Bearer ${accessToken}`
        }
      });
    }

    return next.handle(clonedRequest).pipe(
      catchError((error: HttpErrorResponse) => {
        // If the error is a 401 Unauthorized, try to refresh the token
        if (error.status === 401) {
          return this.tokenService.refreshAccessToken().pipe(
            switchMap((newAccessToken) => {
              // Retry the failed request with the new access token
              clonedRequest = req.clone({
                setHeaders: {
                  Authorization: `Bearer ${newAccessToken}`
                }
              });
              return next.handle(clonedRequest);
            }),
            catchError((refreshError) => {
              // If refreshing the token fails, clear tokens and rethrow the error
              console.error('Error refreshing token:', refreshError);
              this.tokenService.clearTokens();
              return throwError(() => refreshError);
            })
          );
        }
        // For other errors, rethrow the original error
        return throwError(() => error);
      })
    );
  }
}
