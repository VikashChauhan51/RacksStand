import { Component, OnInit } from '@angular/core';
import { CustomerService } from './shared/customer.service'; 
@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  providers: [CustomerService]
})
export class CustomersComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
