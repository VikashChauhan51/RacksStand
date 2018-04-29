import { Component, OnInit } from '@angular/core';
import { SessionService } from '../../core/session.service';
import { Subscription } from 'rxjs/Subscription';
import { ActivatedRoute, Router } from '@angular/router';
class MenuItem {
    constructor(public caption: string, public link: any[], public visible: boolean, public active: string) { }
}


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html'
})
export class NavComponent implements OnInit {
    menuItems: MenuItem[];
    FullName: string;
     private navSub: Subscription;
  constructor(private sessionService: SessionService,private router: Router) { }

  ngOnInit() {
      this.menuItems = [
          { caption: 'Dashboard', link: ['/dashboard'], visible: true, active:'active' },
          { caption: 'Login', link: ['/login'], visible: false, active:'' },
          { caption: 'Customers', link: ['/customers'], visible: true,active:'' },
          { caption: 'Suppliers', link: ['/suppliers'], visible: true,active:'' },
          { caption: 'Stores', link: ['/stores'], visible: true,active:'' },
           { caption: 'Currencies', link: ['/currencies'], visible: true,active:'' } ,
           { caption: 'Taxes', link: ['/taxes'], visible: true,active:'' }
      ];
    this.setUserData();
    this.setActive();
  }
  private setUserData() {
      let res = JSON.parse(this.sessionService.getSession());
      this.FullName = `${res.FirstName}  ${res.LastName}`; 
      
  }
  private getSessionId():string {
      let res = JSON.parse(this.sessionService.getSession());
      return res.SessionId; 
      
  }
  private setActive()
  {
      let url = window.location.pathname.replace('/', '');
            let path = '';
            let array = url.split('/') || [];
            if (array != undefined && array != null && array.length > 0)
                path = array[0];
         if (path == undefined || path == null || path == '' || path.length <= 0 || path == 'resetPassword' || path == 'forgotPassword' || path == 'login')
            path = 'dashboard';
             this.menuItems.forEach(x => {
          if (x.link[0].replace('/', '') ==path)
              x.active = 'active';
          else
              x.active = '';
      });
  }
  public onClick(event, item) {
      this.menuItems.forEach(x => {
          if (x.caption == item.caption)
              x.active = 'active';
          else
              x.active = '';
      });
  }
  logout()
  {
      this.navSub=this.sessionService.logout().subscribe(result=>
      {
        if(result.IsSucceed)
        {
        this.sessionService.clearSession();
         this.router.navigate(['/login']);
        }
      });
  }
}
