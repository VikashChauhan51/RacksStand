import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { TokenService } from '../shared/services/token.service';

@Component({
  selector: 'app-header',
  imports: [RouterModule, CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements OnInit {
  isLoggedIn: boolean = false;
  userName: string = 'John Doe';

  silos = [
    { name: 'Silo 1', actions: ['Action 1', 'Action 2', 'Action 3'] },
    { name: 'Silo 2', actions: ['Action 1', 'Action 2', 'Action 3'] },
    { name: 'Silo 3', actions: ['Action 1', 'Action 2', 'Action 3'] },
  ];

  isSubNavVisible = false;
  selectedSiloActions: string[] = [];

  constructor(private tokenService: TokenService) {}

  toggleSubNav(silo: { name: string, actions: string[] }) {
    this.isSubNavVisible = !this.isSubNavVisible;
    this.selectedSiloActions = silo.actions;
  }
  ngOnInit(): void {
    // Check if the user is logged in
    this.isLoggedIn = this.tokenService.isLoggedIn();
  }

  // Logout function to clear tokens and update the login state
  logout(): void {
    this.tokenService.logout().subscribe(() => {
      this.isLoggedIn = false;
    });
  }
}
