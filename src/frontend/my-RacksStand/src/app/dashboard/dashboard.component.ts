import { Component, OnInit } from '@angular/core';
import { SpinnerService,SpinnerType } from '../core/spinner/spinner.service';
import { ModalService, ModalType } from '../core/modal/modal.service';
import { ToastService,ToastType } from '../core/toast/toast.service';
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(private spinnerService:SpinnerService,private modalService:ModalService,private toastService:ToastService) { }

  ngOnInit() {
  }
  
   
}
