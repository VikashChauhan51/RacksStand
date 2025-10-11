import { Component, Output, EventEmitter,ElementRef, Renderer2 } from '@angular/core';
import { NavService } from '../../services/nav.service';
import { NavItem } from '../../models/nav-item';

@Component({
  selector: 'app-header',
  imports: [],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent {
  @Output() navClick = new EventEmitter<string>();
  constructor(private navService: NavService,private el: ElementRef, private renderer: Renderer2) {}

  handleNavClick(key: string) {
    this.navService.setSubMenuItems(key);
    this.navClick.emit(key);
    this.collapseNavbar();
  }

  collapseNavbar() {
    const navbarCollapse =
      this.el.nativeElement.querySelector('#bdNavbar');
    if (navbarCollapse.classList.contains('show')) {
      this.renderer.removeClass(navbarCollapse, 'show');
    }
  }
}
