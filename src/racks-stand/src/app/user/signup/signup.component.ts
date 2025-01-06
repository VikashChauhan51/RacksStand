import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {
  signupForm!: FormGroup; // Main form group
  errorMessage: string | null = null;
  currentStep: number = 1; // To track the current step in the form

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    // Form Group with two sections: User Info and Company Info
    this.signupForm = this.fb.group({
      // Step 1 - User Info
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]],

      // Step 2 - Company Info
      companyName: ['', Validators.required],
      companyAddress: ['', Validators.required]
    }, {
      validator: this.passwordMatchValidator
    });
  }

  get f() {
    return this.signupForm.controls;
  }

  // Custom validator to check password match
  passwordMatchValidator(group: FormGroup) {
    const password = group.get('password')?.value;
    const confirmPassword = group.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { match: true };
  }

  // Method to navigate to the next step
  nextStep() {
    if (this.currentStep < 2) {
      this.currentStep++;
    }
  }

  // Method to navigate to the previous step
  prevStep() {
    if (this.currentStep > 1) {
      this.currentStep--;
    }
  }

  // Submit the form when all steps are completed
  submitForm() {
    if (this.signupForm.valid) {
      console.log('Form Submitted', this.signupForm.value);
    } else {
      this.errorMessage = 'Please fill all the fields correctly.';
    }
  }
}
