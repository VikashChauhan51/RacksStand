import axios, { AxiosError, type InternalAxiosRequestConfig, type AxiosResponse } from "axios";
import { config } from "../config/config";
import type { ProblemDetails } from "../types/api";

const axiosClient = axios.create({
  baseURL: config.api.baseUrl,
  timeout: 10000,
  headers: { "Content-Type": "application/json" },
});

// Request Interceptor
axiosClient.interceptors.request.use((requestConfig: InternalAxiosRequestConfig) => {
  const token = localStorage.getItem("authToken");
  if (token) {
    requestConfig.headers.Authorization = `Bearer ${token}`;
  }
  return requestConfig;
});

// Response Interceptor with strong typing
axiosClient.interceptors.response.use(
  (response:AxiosResponse) => response,
  (error: AxiosError<ProblemDetails>) => {
    return Promise.reject(error);
  }
);

export default axiosClient;
