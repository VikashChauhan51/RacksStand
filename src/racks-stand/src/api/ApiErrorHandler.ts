import { AxiosError } from "axios";
import type { ProblemDetails } from "../types/api";
import { type NavigateFunction } from "react-router";


export class ApiErrorHandler {
  private navigate?: NavigateFunction;

  // Method to inject dependencies
  setNavigate(navigate: NavigateFunction) {
    this.navigate = navigate;
  }


  handle(error: AxiosError<ProblemDetails>) {

     if (!this.navigate) {
      throw new Error("Navigate function not set in ApiErrorHandler");
    }

    const status = error.response?.status;

    switch (status) {
      case 401:
        this.navigate("/signin");
        break;
      case 403:
        this.navigate("/forbidden");
        break;
      case 404:
        this.navigate("/not-found");
        break;
      case 500:
        this.navigate("/server-error");
        break;
      default:
        console.error("Unhandled API error:", error);
    }
  }
}
