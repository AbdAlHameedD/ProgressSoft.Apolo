import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BusinessCardComponent } from './business-card/business-card.component';
import { NotFoundComponent } from '../shared-components/not-found/not-found.component';

const routes: Routes = [
  {
    path: 'businesscard',
    component: BusinessCardComponent,
    title: 'Apolo - Business Card'
  },
  {
    path: '',
    redirectTo: '/home/businesscard',
    pathMatch: 'full'
  },
  {
    path: '**',
    component: NotFoundComponent,
    title: 'Apolo - Page Not Found'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
