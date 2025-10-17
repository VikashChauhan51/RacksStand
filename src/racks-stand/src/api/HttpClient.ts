import axios from "axios";
import type {
  AxiosError,
  AxiosInstance,
  AxiosRequestConfig,
  AxiosResponse,
  InternalAxiosRequestConfig,
} from "axios";
import { config } from "../config/config";
import { TokenService } from "./TokenService";
import type { ProblemDetails } from "../types/api";

// -------------------------------------------------------------
// Extend AxiosRequestConfig safely (typed retry & control flags)
// -------------------------------------------------------------
declare module "axios" {
  export interface AxiosRequestConfig {
    _retry?: boolean;
    _retryCount?: number;
    skipGlobalErrorHandler?: boolean;
    retryAttempts?: number;
    retryDelay?: number;
  }
}

// -------------------------------------------------------------
// Factory pattern for per-instance state encapsulation
// -------------------------------------------------------------
const createHttpClient = (): AxiosInstance => {
  let refreshPromise: Promise<string | null> | null = null;

  const httpClient = axios.create({
    baseURL: config.api.baseUrl,
    timeout: 10000,
    headers: { "Content-Type": "application/json" },
  });

  // -----------------------------------------------------------
  // REQUEST interceptor
  // -----------------------------------------------------------
  httpClient.interceptors.request.use(
    (request: InternalAxiosRequestConfig) => {
      const token = TokenService.getAccessToken();
      if (token) {
        request.headers.Authorization = `Bearer ${token}`;
      }
      return request;
    },
    (error) => Promise.reject(error)
  );

  // -----------------------------------------------------------
  // RESPONSE interceptor
  // -----------------------------------------------------------
  httpClient.interceptors.response.use(
    (response: AxiosResponse) => response,
    async (error: AxiosError<ProblemDetails>) => {
      const originalRequest = error.config as AxiosRequestConfig;
      const status = error.response?.status;

      // Network or CORS issue
      if (!status) {
        console.error("Network or CORS error:", error.message);
        return Promise.reject(error);
      }

      // 429 Too Many Requests
      if (status === 429) {
        if (!originalRequest.skipGlobalErrorHandler) {
          alert("Too many requests. Please try again later.");
        }
        return Promise.reject(error);
      }

      // 401 Unauthorized → refresh token logic
      if (status === 401 && !originalRequest._retry) {
        originalRequest._retry = true;

        try {
          if (!refreshPromise) {
            const refreshToken = TokenService.getRefreshToken();
            if (!refreshToken) throw new Error("Missing refresh token");

            // one shared refresh promise
            refreshPromise = (async () => {
              const response = await axios.post(
                `${config.api.baseUrl}/auth/refresh`,
                { refreshToken }
              );

              const newToken = response.data?.accessToken;
              if (!newToken) throw new Error("Invalid refresh response");

              TokenService.setAccessToken(newToken);
              return newToken;
            })()
              .catch(() => {
                TokenService.clearTokens();
                window.location.href = "/signin";
                return null;
              })
              .finally(() => {
                refreshPromise = null;
              });
          }

          const newToken = await refreshPromise;
          if (!newToken) throw new Error("Failed to refresh token");

          // retry original request
          if (originalRequest.headers) {
            originalRequest.headers.Authorization = `Bearer ${newToken}`;
          }
          return httpClient(originalRequest);
        } catch (refreshError) {
          return Promise.reject(refreshError);
        }
      }

      // 502, 503, 504 → transient server retry
      if ([502, 503, 504].includes(status)) {
        const maxAttempts = originalRequest.retryAttempts ?? 3;
        const delayMs = originalRequest.retryDelay ?? 500;
        originalRequest._retryCount = (originalRequest._retryCount || 0) + 1;

        if (originalRequest._retryCount <= maxAttempts) {
          await new Promise((r) => setTimeout(r, delayMs));
          return httpClient(originalRequest);
        }
      }

      // 400/422 → return to caller (validation or domain errors)
      if (status === 400 || status === 422) {
        return Promise.reject(error);
      }

      // Other global errors
      if (!originalRequest.skipGlobalErrorHandler) {
        console.error("Global API Error:", error.response);
      }

      return Promise.reject(error);
    }
  );

  return httpClient;
};

// export shared instance
export const HttpClient = createHttpClient();
export default HttpClient;
