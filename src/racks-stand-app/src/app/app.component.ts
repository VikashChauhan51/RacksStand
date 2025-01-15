import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { InfoBarComponent } from './components/info-bar/info-bar.component';
import { HeaderComponent } from './components/header/header.component';
import { SubHeaderComponent } from './components/sub-header/sub-header.component';
import { FooterComponent } from './components/footer/footer.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,InfoBarComponent,HeaderComponent,SubHeaderComponent,FooterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'racks-stand-app';
}
