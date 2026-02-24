/**
 * Represents the RFC 7807 Problem Details response commonly returned by ASP.NET Core.
 */
export interface ProblemDetails {
  type?: string;
  title?: string;
  status?: number;
  detail?: string;
  instance?: string;
  traceId?: string;
 errors?: Record<string, string[]>;
}

/**
 * Represents a validation error returned by ASP.NET Core model validation.
 */
export interface ValidationProblemDetails extends ProblemDetails {
  errors?: Record<string, string[]>;
}

/**
 * Represents an Axios error response structure including ProblemDetails.
 */
export interface ApiError {
  /** HTTP status code (e.g., 400, 404, 500) */
  status: number;
  /** The URL or endpoint where the error occurred */
  path?: string;
  /** Message derived from backend or fallback */
  message: string;
  /** The detailed RFC 7807 problem structure */
  problem?: ProblemDetails | ValidationProblemDetails;
}
