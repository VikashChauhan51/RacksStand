import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Subject } from 'rxjs/Subject';

export interface ResetMessage {
  message: string;
}
@Injectable()
export class MessageService{

 private subject = new Subject<ResetMessage>();

  constructor( ) { }

      clearMessage() {
        this.subject.next();
    }
  showMessage(text:string) {
      let msg = text ||'Hi';
        this.subject.next({ message: msg });
        

}
