import { Component } from '@angular/core';
import { BusinessCardFilter } from '../dtos/business-card-filter.dto';
import { BusinessCard } from '../models/business-card.model';
import { Result } from '../models/result';
import { BusinessCardService } from '../services/business-card.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  standalone: false
})
export class AppComponent {
  title = 'Apolo';
}
