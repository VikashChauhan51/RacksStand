import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import {ValidationErrorComponent} from '../../shared/components/validation-error/validation-error.component'

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule,ValidationErrorComponent],
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
      name: ['', [Validators.required,Validators.maxLength(100)]],
      email: ['', [Validators.required, Validators.email,Validators.maxLength(150)]],
      password: ['', [Validators.required, Validators.minLength(6),Validators.maxLength(50)]],
      companyName: ['', [Validators.required, Validators.maxLength(150)]],
      address1: ['', [Validators.required, Validators.maxLength(250)]],
      address2: ['',[Validators.maxLength(250)]],
      state: ['', [Validators.required,Validators.maxLength(100)]],
      city: ['', [Validators.required,Validators.maxLength(100)]],
      country: ['', [Validators.required,Validators.maxLength(100)]],
      zipcode: ['', [Validators.required,Validators.maxLength(15)]]
    });
  }

  get f() {
    return this.signupForm.controls;
  }

  step1ControlsAreValid(): boolean {
    return this.f['name'].valid && this.f['email'].valid && this.f['password'].valid;
  }
  // Method to navigate to the next step
  nextStep() {
    if(this.step1ControlsAreValid() && this.currentStep < 2){
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
