import { Component, Input } from '@angular/core';
import { AbstractControl } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ValidationMessages, ValidationMessageKey } from '../../configs/validation-messages.config';

@Component({
  selector: 'app-validation-error',
  imports:[CommonModule],
  templateUrl: './validation-error.component.html',
  styleUrls: ['./validation-error.component.scss']
})
export class ValidationErrorComponent {
  @Input() control: AbstractControl | null = null;
  @Input() fieldName: string = 'This field'; // Default field name

  /**
   * Get the keys of validation errors for the control
   */
  getErrorKeys(): string[] {
    return this.control ? Object.keys(this.control.errors || {}) : [];
  }

  /**
   * Get the formatted error message for a specific validation error key
   * @param errorKey - The validation error key
   * @returns The formatted error message
   */
  getErrorMessage(errorKey: string): string {
    const error = this.control?.errors?.[errorKey];
    const messageTemplate = ValidationMessages[errorKey as ValidationMessageKey] || 'Invalid field.';

    // Replace placeholders (e.g., {{field}}, {{requiredLength}})
    let formattedMessage = messageTemplate.replace('{{field}}', this.fieldName);

    if (error && typeof error === 'object') {
      Object.keys(error).forEach((key) => {
        formattedMessage = formattedMessage.replace(`{{${key}}}`, error[key]);
      });
    }

    return formattedMessage;
  }
}
