import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-login',
  imports:[CommonModule,FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  errorMessage: string = '';

  login(): void {
    if (this.email && this.password) {
      // Add login logic here (e.g., make an API call)
      console.log('Logging in...', this.email, this.password);
    } else {
      this.errorMessage = 'Please fill in all fields.';
    }
  }
}

