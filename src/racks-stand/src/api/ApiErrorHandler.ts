import type { AxiosError } from "axios";
import type { NavigateFunction } from "react-router";
import type { ProblemDetails } from "../types/api";

export const ApiErrorHandler = (
  navigate: NavigateFunction,
  error: AxiosError<ProblemDetails>
) => {
  const status = error.response?.status;

  switch (status) {
    case 401:
      navigate("/signin");
      break;
    case 403:
      navigate("/forbidden");
      break;
    case 404:
      navigate("/not-found");
      break;
    case 500:
      navigate("/server-error");
      break;
    case 429:
      alert("Too many requests. Please try again later.");
      break;
    default:
      throw error;
  }
};
