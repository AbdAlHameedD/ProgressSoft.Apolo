import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideHttpClient } from '@angular/common/http';
import { NavbarComponent } from './shared-components/navbar/navbar.component';
import { BusinessCardComponent } from './business-card/business-card.component';
import { BusinessCardService } from '../services/business-card.service';
import { FooterComponent } from './shared-components/footer/footer.component';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    BusinessCardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NavbarComponent,
    FooterComponent,
    CommonModule
  ],
  providers: [
    provideClientHydration(),
    provideHttpClient(),
    BusinessCardService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
