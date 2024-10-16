import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BusinessCardComponent } from './business-card/business-card.component';
import { NotFoundComponent } from '../shared-components/not-found/not-found.component';

const routes: Routes = [];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
