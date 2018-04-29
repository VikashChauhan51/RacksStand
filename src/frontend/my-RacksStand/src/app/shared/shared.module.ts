import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule ,ReactiveFormsModule} from '@angular/forms';
import { InitCapsPipe } from './string-pipe/init-caps.pipe';
import { SearchBox } from './search/search.directive';
import { BusyIndicator } from './busy-indicator/busy-indicator.directive';
import { LoadMore } from './load-more/load-more.directive';
import { NguiAutoCompleteComponent } from './auto-complete/auto-complete.component';
import { NguiAutoCompleteDirective } from './auto-complete/auto-complete.directive';
import { NguiAutoComplete } from './auto-complete/auto-complete';
@NgModule({
  imports: [
      CommonModule, FormsModule,ReactiveFormsModule
    ],
  exports: [CommonModule, FormsModule,ReactiveFormsModule, InitCapsPipe,SearchBox,BusyIndicator,LoadMore,NguiAutoCompleteComponent,NguiAutoCompleteDirective],
  declarations: [InitCapsPipe,SearchBox,BusyIndicator,LoadMore,NguiAutoCompleteComponent,NguiAutoCompleteDirective],
  providers: [NguiAutoComplete],
  entryComponents: [NguiAutoCompleteComponent]
})
export class SharedModule { }
