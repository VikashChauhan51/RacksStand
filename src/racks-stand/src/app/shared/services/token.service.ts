import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  private accessTokenKey = 'access_token';
  private refreshTokenKey = 'refresh_token';
  private apiUrl = 'https://api.yourapp.com';  // Replace with your API base URL

  private refreshTokenSubject: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);

  constructor(private http: HttpClient) {}

  // Store tokens in localStorage
  storeTokens(accessToken: string, refreshToken: string): void {
    localStorage.setItem(this.accessTokenKey, accessToken);
    localStorage.setItem(this.refreshTokenKey, refreshToken);
  }

  // Retrieve access token from localStorage
  getAccessToken(): string | null {
    return localStorage.getItem(this.accessTokenKey);
  }

  // Retrieve refresh token from localStorage
  getRefreshToken(): string | null {
    return localStorage.getItem(this.refreshTokenKey);
  }

  // Remove tokens from localStorage
  clearTokens(): void {
    localStorage.removeItem(this.accessTokenKey);
    localStorage.removeItem(this.refreshTokenKey);
  }

  // Refresh the access token using the refresh token
  refreshAccessToken(): Observable<any> {
    const refreshToken = this.getRefreshToken();

    if (!refreshToken) {
      throw new Error('No refresh token available');
    }

    return this.http.post(`${this.apiUrl}/auth/refresh`, { refreshToken }).pipe(
      switchMap((response: any) => {
        const newAccessToken = response.accessToken;
        this.storeTokens(newAccessToken, refreshToken);  // Store new access token
        return [newAccessToken];
      }),
      catchError((error) => {
        console.error('Error refreshing access token:', error);
        throw error;
      })
    );
  }

  // Intercept API requests to attach the access token
  addAuthHeader(request: any) {
    const accessToken = this.getAccessToken();
    if (accessToken) {
      request.headers = request.headers.set('Authorization', `Bearer ${accessToken}`);
    }
    return request;
  }
}
