// Define the keys for validation messages
export type ValidationMessageKey =
  | 'required'
  | 'maxlength'
  | 'minlength'
  | 'email'
  | 'phone'
  | 'pattern'
  | 'customError';

// Define a type-safe structure for validation messages
export type ValidationMessagesType = Record<ValidationMessageKey, string>;

// Implement the validation messages
export const ValidationMessages: ValidationMessagesType = {
  required: '{{field}} is required.',
  maxlength: '{{field}} cannot exceed {{requiredLength}} characters.',
  minlength: '{{field}} must be at least {{requiredLength}} characters.',
  email: 'Please enter a valid email address.',
  phone: 'Please enter a valid phone number.',
  pattern: '{{field}} format is invalid.',
  customError: '{{field}} has a custom validation error.',
};

// Utility function to get validation messages
export function getValidationMessage(key: ValidationMessageKey): string {
  return ValidationMessages[key];
}
