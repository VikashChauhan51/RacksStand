import { useNavigate } from "react-router";
import HttpClient from "../api/HttpClient";
import { ApiErrorHandler } from "../api/ApiErrorHandler";
import type { AxiosError, AxiosRequestConfig } from "axios";
import type { ProblemDetails } from "../types/api";

export function useApiService() {
  const navigate = useNavigate();

  const handleError = (error: AxiosError<ProblemDetails>) => {
    ApiErrorHandler(navigate, error);
    throw error; // rethrow to let React Query handle it too
  };

  const request = async <T>(config: AxiosRequestConfig): Promise<T> => {
    try {
      const response = await HttpClient.request<T>(config);
      return response.data;
    } catch (err) {
      handleError(err as AxiosError<ProblemDetails>);
      throw err;
    }
  };

  return {
    request,
    get: <T>(url: string, params?: unknown) => request<T>({ method: "get", url, params }),
    post: <T>(url: string, data?: unknown) => request<T>({ method: "post", url, data }),
    put: <T>(url: string, data?: unknown) => request<T>({ method: "put", url, data }),
    delete: <T>(url: string) => request<T>({ method: "delete", url }),
  };
}
