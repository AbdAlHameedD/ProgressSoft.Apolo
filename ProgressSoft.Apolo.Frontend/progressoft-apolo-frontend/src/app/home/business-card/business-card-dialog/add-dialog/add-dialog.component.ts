import { Component, ElementRef, Inject, ViewChild } from '@angular/core';
import { BusinessCardService } from '../../../../../services/business-card.service';
import {  MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ImageService } from '../../../../../services/image.service';
import { BusinessCardDialog } from '../business-card-dialog';
import { BusinessCard } from '../../../../../models/business-card.model';

@Component({
  selector: 'app-add-dialog',
  templateUrl: '../business-card-dialog.component.html',
  styleUrl: '../business-card-dialog.component.scss'
})

export class AddDialogComponent extends BusinessCardDialog {
  public confirmButtonLabel: string = 'Add';
  @ViewChild('uploadPhoto') uploadPhoto: ElementRef | undefined;

  constructor(@Inject(MAT_DIALOG_DATA) public data: BusinessCard,
    businessCardService: BusinessCardService,
    imageService: ImageService,
    dialogRef: MatDialogRef<AddDialogComponent>) {

    super( imageService, dialogRef, businessCardService.add.bind(businessCardService));
  }

  changeImage(): void {
    this.uploadPhoto?.nativeElement.click();
  }
}
