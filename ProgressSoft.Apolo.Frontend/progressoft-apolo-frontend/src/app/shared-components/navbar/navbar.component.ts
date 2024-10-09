import { Component } from '@angular/core';
import { Environment } from '../../../constants/environment';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
  standalone: true
})
export class NavbarComponent {
  public readonly title: string = Environment.WEBSITE_TITLE;
}
