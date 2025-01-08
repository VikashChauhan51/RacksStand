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
  activeSubMenu: string | null = null;
  isProfileMenuVisible = false;

  constructor(private tokenService: TokenService) {}

  toggleProfileMenu() {
    this.isProfileMenuVisible = !this.isProfileMenuVisible;
  }
  toggleSubMenu(menu: string) {
    this.activeSubMenu = this.activeSubMenu === menu ? null : menu;
  }

  isSubMenuVisible(menu: string) {
    return this.activeSubMenu === menu;
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
