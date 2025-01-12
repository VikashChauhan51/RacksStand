export interface Outcome<T> {
  isSuccess: boolean;
  isFailure: boolean;
  value?: T | null;
  errors: OutcomeError[];
}

export interface OutcomeError {
  message: string;
  code?: string;
}
