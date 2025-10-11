import { Component, Input, OnInit } from '@angular/core';
import { NavService } from '../../services/nav.service';
import { NavItem } from '../../models/nav-item';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-sub-header',
  imports: [CommonModule],
  templateUrl: './sub-header.component.html',
  styleUrl: './sub-header.component.css',
})
export class SubHeaderComponent implements OnInit {
  @Input() navKey: string='';
  subMenuItems: NavItem[] = [];
  constructor(private navService: NavService) {}
  ngOnInit(): void {
    this.navService.subMenuItems$.subscribe((items) => {
      this.subMenuItems = items;
    });
  }

  ngOnChanges(): void {
    if (this.navKey) {
      this.navService.setSubMenuItems(this.navKey);
    }
  }
}
