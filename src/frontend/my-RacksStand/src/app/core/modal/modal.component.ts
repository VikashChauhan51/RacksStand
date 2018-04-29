import { Component, OnInit, HostListener } from '@angular/core';
import { ModalService, ModalType } from './modal.service';

const KEY_ESC = 27;

@Component({
    selector: 'app-modal',
    templateUrl: 'modal.component.html'
})
export class ModalComponent implements OnInit {
    title: string;
    message: string;
    mode: ModalType;
    showCancelButton: boolean = false;
    iconClass: string;
    negativeOnClick: () => void;
    positiveOnClick: () => void;

    private defaults = {
        title: 'Confirmation',
        message: 'Do you want to cancel your changes?',
        mode: ModalType.Confirm
    };
    private modalElement: any;
    constructor(modalService: ModalService) {
        modalService.activate = this.activate.bind(this);
    }

    activate(message = this.defaults.message, title = this.defaults.title, mode = this.defaults.mode) {
        this.message = message;
        this.title=title;
        this.mode = mode;
        this.setData();
        //show cancel button only when dialog type is confirmation type.
        this.showCancelButton = (this.mode === ModalType.Confirm) ? true : false;
        let promise = new Promise<boolean>((resolve, reject) => {
            this.negativeOnClick = () => resolve(false);
            this.positiveOnClick = () => resolve(true);
            //show popup.
            this.show();
        });

        return promise;
    }
    OK() {
        this.hideDialog();
        this.positiveOnClick();
    }
    Cancel() {
        this.hideDialog();
        this.negativeOnClick();
    }

    ngOnInit() {
        this.modalElement = document.getElementById('myModal');
    }

    @HostListener('document:keyup', ['$event'])
    onKeyUp(ev: KeyboardEvent) {
        if (ev.keyCode === KEY_ESC) {
            this.Cancel();
        }
    }
    private setData() {
        switch (this.mode) {
            case 0: //Success
                this.iconClass = 'glyphicon glyphicon-check';
                this.title = 'Success';
                break;
            case 1: //Info
                this.iconClass = 'glyphicon glyphicon-info-sign';
                this.title = 'Information';
                break;
            case 2: //Warning
                this.iconClass = 'glyphicon glyphicon-warning-sign';
                this.title = 'Warning';
                break;
            case 3://Danger
                this.iconClass = 'glyphicon glyphicon-fire';
                this.title = 'Wrong';
                break;
            case 4: //Confirm
            default:
                this.iconClass = 'glyphicon glyphicon-check';
                this.title = 'Confirmation';
                break;
        }
    }
    private show() {

        this.modalElement.style.display = "block";
    }
    private hideDialog() {
        this.modalElement.style.display = "none";

    }

}

