import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideHttpClient } from '@angular/common/http';
import { NavbarComponent } from './shared-components/navbar/navbar.component';
import { BusinessCardService } from '../services/business-card.service';
import { FooterComponent } from './shared-components/footer/footer.component';
import { CommonModule } from '@angular/common';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE, provideNativeDateAdapter} from '@angular/material/core';
import { Environment } from '../constants/environment';
import {MomentDateAdapter} from '@angular/material-moment-adapter';
import { ImageService } from '../services/image.service';
import { NotFoundComponent } from './shared-components/not-found/not-found.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NavbarComponent,
    FooterComponent,
    CommonModule,
    NotFoundComponent
  ],
  providers: [
    provideClientHydration(),
    provideHttpClient(),
    BusinessCardService,
    provideAnimationsAsync(),
    provideNativeDateAdapter(),
    {provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE]},
    { provide: MAT_DATE_FORMATS, useValue: Environment.DATE_FORMATS },
    ImageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
