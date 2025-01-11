import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { AuthService } from './auth.service'; // Inject AuthService

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  private accessTokenKey = 'access_token';
  private refreshTokenKey = 'refresh_token';

  constructor(private authService: AuthService) {}

  // Store tokens in localStorage
  storeTokens(accessToken: string, refreshToken: string): void {
    localStorage.setItem(this.accessTokenKey, accessToken);
    localStorage.setItem(this.refreshTokenKey, refreshToken);
  }

  isLoggedIn(): boolean {
    const accessToken = this.getAccessToken();
    return !!accessToken; // If access token exists, user is logged in
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
  refreshAccessToken(): Observable<string> {
    const refreshToken = this.getRefreshToken();

    if (!refreshToken) {
      throw new Error('No refresh token available');
    }

    return this.authService.refreshToken(refreshToken).pipe(
      map((response: any) => {
        const newAccessToken = response.accessToken;
        const newRefreshToken = response.refreshToken || refreshToken; // Update refresh token if provided
        this.storeTokens(newAccessToken, newRefreshToken); // Store new tokens
        return newAccessToken;
      }),
      catchError((error) => {
        console.error('Error refreshing access token:', error);
        throw error;
      })
    );
  }

  // Login user and store tokens
  login(email: string, password: string): Observable<void> {
    return this.authService.login(email, password).pipe(
      map((response: any) => {
        const { accessToken, refreshToken } = response;
        this.storeTokens(accessToken, refreshToken); // Store tokens
      }),
      catchError((error) => {
        console.error('Error during login:', error);
        throw error;
      })
    );
  }

  // Logout user and clear tokens
  logout(): Observable<void> {
    const accessToken = this.getAccessToken() || '';
    const refreshToken = this.getRefreshToken() || '';
    return this.authService.logout(accessToken, refreshToken).pipe(
      map(() => {
        this.clearTokens(); // Clear tokens from local storage
      }),
      catchError((error) => {
        console.error('Error during logout:', error);
        throw error;
      })
    );
  }
}
