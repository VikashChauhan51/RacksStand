import { Component, OnDestroy, OnInit } from '@angular/core';
import { ToastService,ToastType } from './toast.service';

@Component({
  selector: 'app-toast',
  templateUrl: './toast.component.html',
  styleUrls: ['./toast.component.css']
})
export class ToastComponent implements  OnInit {
    message: string;
    private toastElement: any;
    constructor(private toastService: ToastService) {
        toastService.activate = this.activate.bind(this);
    }

      
    activate(message: string,type?:ToastType) {
        this.message = message;
        this.showTost(type);
    }
 
    ngOnInit() {
        this.toastElement = document.getElementById('snackbar');
    }

   
    private hide() {
        this.toastElement.className = this.toastElement.className.replace("show", "");
    }
    private showTost(type?:ToastType) {
     
        switch  (type) {
            case 0: //success
               this.toastElement.className = "show snackbar-success col-sx-12 col-sm-3 ";
            break;
             case 1: //info
                this.toastElement.className = "show snackbar-info col-sx-12 col-sm-3 ";
            break;
             case 2://warning
                this.toastElement.className = "show snackbar-warning col-sx-12 col-sm-3 ";
            break;
             case 3://danger
                this.toastElement.className = "show snackbar-danger col-sx-12 col-sm-3 ";
            break;
            default:
               this.toastElement.className = "show snackbar-primary col-sx-12 col-sm-3 ";
            break;

        };
        window.setTimeout(() => this.hide(), 2500);
    }
 
}
