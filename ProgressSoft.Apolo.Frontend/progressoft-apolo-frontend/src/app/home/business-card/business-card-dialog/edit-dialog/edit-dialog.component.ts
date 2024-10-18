import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BusinessCard } from '../../../../../models/business-card.model';
import { BusinessCardService } from '../../../../../services/business-card.service';
import { Gender } from '../../../../../enums/Gender';
import { ImageService } from '../../../../../services/image.service';
import { Result } from '../../../../../models/result';
import { Image } from '../../../../../models/image.model';
import { BusinessCardDialog } from '../business-card-dialog';

@Component({
  selector: 'app-edit-dialog',
  templateUrl: '../business-card-dialog.component.html',
  styleUrl: '../business-card-dialog.component.scss'
})
export class EditDialogComponent extends BusinessCardDialog implements OnInit {
  public confirmButtonLabel: string = 'Edit';
  @ViewChild('uploadPhoto') uploadPhoto: ElementRef | undefined;
  
  constructor(@Inject(MAT_DIALOG_DATA) public data: BusinessCard,
    businessCardService: BusinessCardService,
    imageService: ImageService,
    dialogRef: MatDialogRef<EditDialogComponent>) { 

      super(imageService, dialogRef, businessCardService.edit.bind(businessCardService));
    }

  ngOnInit(): void {
    this.formGroup.patchValue({
      id: this.data.id, 
      name: this.data.name,
      birthOfDate: this.data.birthOfDate,
      gender: (<Gender>this.data.gender).toString(),
      phone: this.data.phone,
      email: this.data.email,
      address: this.data.address,
      imageId: this.data.imageId
    });

    this.imageService.getById(this.data.imageId!).subscribe(result => {
      const mappedResult: Result<Image> = result as Result<Image>;

      this.imagePath = `data:${mappedResult.data!.type};base64,${mappedResult.data!.encodedImage}`;
    });
  }

  changeImage(): void {
    this.uploadPhoto?.nativeElement.click();
  }
}
