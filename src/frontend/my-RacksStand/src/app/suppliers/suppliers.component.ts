import { Component, OnInit } from '@angular/core';
import { SupplierService } from './shared/supplier.service'; 
@Component({
  selector: 'app-suppliers',
  templateUrl: './suppliers.component.html',
  providers: [SupplierService]
})
export class SuppliersComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
