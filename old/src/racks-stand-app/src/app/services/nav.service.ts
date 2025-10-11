import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { NavItem } from '../models/nav-item';

@Injectable({
  providedIn: 'root'
})
export class NavService {
  private subNavData: { [key: string]: NavItem[] } = {
    home: [{ name: 'Sub1', href: '#sub1' }, { name: 'Sub2', href: '#sub2' }],
    about: [{ name: 'Sub3', href: '#sub3' }, { name: 'Sub4', href: '#sub4' }],
    contact: [{ name: 'Sub5', href: '#sub5' }, { name: 'Sub6', href: '#sub6' }]
  };

  private subMenuItemsSource = new BehaviorSubject<NavItem[]>([]);
  subMenuItems$ = this.subMenuItemsSource.asObservable();

  setSubMenuItems(key: string) {
    const items = this.subNavData[key] || [];
    this.subMenuItemsSource.next(items);
  }
}
