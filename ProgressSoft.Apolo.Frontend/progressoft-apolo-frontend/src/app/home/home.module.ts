import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeRoutingModule } from './home-routing.module';
import { BusinessCardComponent } from './business-card/business-card.component';
import { MatFormField, MatInputModule } from '@angular/material/input';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatFormFieldModule} from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import {MatSelectModule} from '@angular/material/select';
import {MatButtonModule} from '@angular/material/button';
import {MatTableModule} from '@angular/material/table';
import {MatIconModule} from '@angular/material/icon';
import {MatDialogModule} from '@angular/material/dialog';
import { DeleteDialogComponent } from './business-card/delete-dialog/delete-dialog.component';
import { EditDialogComponent } from './business-card/business-card-dialog/edit-dialog/edit-dialog.component';
import { AddDialogComponent } from './business-card/business-card-dialog/add-dialog/add-dialog.component';
import {MatMenuModule} from '@angular/material/menu';
import { PreviewDialogComponent } from './business-card/preview-dialog/preview-dialog.component';



@NgModule({
  declarations: [
    BusinessCardComponent,
    DeleteDialogComponent,
    EditDialogComponent,
    AddDialogComponent,
    PreviewDialogComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    MatInputModule,
    MatFormField,
    MatDatepickerModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatButtonModule,
    MatTableModule,
    MatIconModule,
    MatDialogModule,
    MatMenuModule
  ]
})
export class HomeModule { }
