import { Component } from '@angular/core';
import { Environment } from '../../../constants/environment';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss',
  standalone: true,
  imports: [CommonModule]
})
export class FooterComponent {
  public readonly title: string = Environment.WEBSITE_TITLE;
  public readonly today: Date = new Date();
}
