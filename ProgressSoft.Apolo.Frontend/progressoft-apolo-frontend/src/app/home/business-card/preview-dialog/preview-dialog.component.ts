import { AfterViewInit, Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { BusinessCard } from '../../../../models/business-card.model';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ExternalBusinessCard } from '../../../../dtos/import-business-card.dto';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Gender } from '../../../../enums/Gender';
import { BusinessCardService } from '../../../../services/business-card.service';
import { Result } from '../../../../models/result';

@Component({
  selector: 'app-preview-dialog',
  templateUrl: './preview-dialog.component.html',
  styleUrl: './preview-dialog.component.scss'
})
export class PreviewDialogComponent implements OnInit {
  public formGroup: FormGroup = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.maxLength(120)]),
    birthOfDate: new FormControl('', [Validators.required]),
    gender: new FormControl('', [Validators.required]),
    phone: new FormControl('', [Validators.required, Validators.maxLength(20)]),
    email: new FormControl('', [Validators.required, Validators.email, Validators.maxLength(320)]),
    address: new FormControl('', [Validators.required, Validators.maxLength(60)]),
    image: new FormControl(null),
    imageType: new FormControl(null)
  });

  public displayedCard: ExternalBusinessCard = new ExternalBusinessCard();
  public currentIndex: number = 0;
  private admittedCards: Array<ExternalBusinessCard> = [];

  @ViewChild('progress') progress: ElementRef | undefined;

  constructor(@Inject(MAT_DIALOG_DATA) public data: Array<ExternalBusinessCard>,
    private dialogRef: MatDialogRef<PreviewDialogComponent>,
    private businessServce: BusinessCardService) {

      this.displayedCard = data[0];
  }

  ngOnInit(): void {
    this.displayNextCard();
  }

  public next(): void {
    if (this.formGroup.valid) {
      const card: ExternalBusinessCard = this.formGroup.value as ExternalBusinessCard;

      this.admittedCards.push(card);
      this.increaseSuccessProgress(this.currentIndex - 1);

      this.displayNextCard();
    }
  }

  public discard(): void {
    this.increaseDiscardProgress(this.currentIndex - 1);
    if (this.isDisplayedLastItem) {
        this.confirm();

    } else {
      this.displayNextCard();
    }
  }

  public cancel(): void {
    this.dialogRef.close(false);
  }

  public confirm(): void {
    this.businessServce.addBulk(this.admittedCards).subscribe(result => {
      const mappedData: Array<BusinessCard> = result.data;
      this.dialogRef.close(mappedData);
    });
  }

  public hasError(controlName: string, validationName: string): boolean {
    return this.formGroup.controls[controlName].hasError(validationName);
  }

  private displayNextCard(): void {
    if (this.currentIndex < this.data.length) {
      this.displayedCard = this.data[this.currentIndex];

      this.patchModelToFormGroup(this.data[this.currentIndex]);

      this.currentIndex++;

    } else {
      this.confirm();
    }
  }

  private patchModelToFormGroup(model: ExternalBusinessCard): void {
    let birthDate: Date | null = null;
    if (model.birthOfDate) {
      birthDate = new Date(model.birthOfDate);
    }

    this.formGroup.patchValue({
      name: model.name,
      birthOfDate: birthDate,
      gender: (<Gender>model.gender)?.toString(),
      phone: model.phone,
      email: model.email,
      address: model.address,
      image: model.image,
      imageType: model.imageType
    });
  }

  private increaseSuccessProgress(index: number): void {
    this.increaseProgress(index, 'bg-success');
  }

  private increaseDiscardProgress(index: number): void {
    this.increaseProgress(index, 'bg-danger');
  }

  private increaseProgress(index: number, classStyle: string): void {
    this.progress?.nativeElement.children[index].classList.add(classStyle);
  }

  get isDisplayedLastItem(): boolean {
    return this.currentIndex === this.data.length;
  }
}
