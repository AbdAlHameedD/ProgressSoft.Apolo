import { Inject } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { BusinessCard } from "../../../../models/business-card.model";
import { ImageService } from "../../../../services/image.service";
import { EditDialogComponent } from "./edit-dialog/edit-dialog.component";
import { AddDialogComponent } from "./add-dialog/add-dialog.component";
import { Observable } from "rxjs";
import { Image } from "../../../../models/image.model";

export abstract class BusinessCardDialog {
    public formGroup: FormGroup = new FormGroup({
        id: new FormControl(),
        name: new FormControl('', [Validators.required, Validators.maxLength(120)]),
        birthOfDate: new FormControl('', [Validators.required]),
        gender: new FormControl('', [Validators.required]),
        phone: new FormControl('', [Validators.required, Validators.maxLength(20)]),
        email: new FormControl('', [Validators.required, Validators.email, Validators.maxLength(320)]),
        address: new FormControl('', [Validators.required, Validators.maxLength(60)]),
        imageId: new FormControl(1)
    });

    public readonly abstract confirmButtonLabel: string;
    public imagePath: string = '/assets/images/blank-profile-picture.png';

    protected imageType?: string | undefined;

    constructor(protected imageService: ImageService,
        protected dialogRef: MatDialogRef<AddDialogComponent | EditDialogComponent>,
        private confirmAction: (businessCard: BusinessCard) => Observable<any>) { }

    public confirm(): void {
        if (this.formGroup.valid) {

            if (this.isDefaultImage || !this.isNewImageUploaded) {
                this.confirmAction(this.formGroup.value).subscribe(result => {
                    this.dialogRef.close(result);
                });
            
            } else {
                const image: Image = {
                    encodedImage: this.imagePath.split('base64,', 2)[1],
                    type: this.imageType
                };
        
                this.imageService.add(image).subscribe(result => {
                    this.formGroup.controls['imageId'].setValue(result.data.id);
        
                    this.confirmAction(this.formGroup.value).subscribe(result => {
                        this.dialogRef.close(result);
                    });
                });
            }
        }
    }

    public cancel(): void {
        this.dialogRef.close(false);
    }

    public hasError(controlName: string, validationName: string): boolean {
        return this.formGroup.controls[controlName].hasError(validationName);
    }
    
    public onImageChanged(event: any): void {
        const file: File = event.target.files[0];
        if (file) {
    
          const fileReader: FileReader = new FileReader();
          fileReader.onload = (e: any): void => {
            
            this.imagePath = e.target.result;
            this.imageType = file.type;
          }
    
          fileReader.readAsDataURL(file);
        }
    }
    
    public removeImage(): void {
        this.formGroup.controls['imageId'].setValue(1);
        this.imagePath = '/assets/images/blank-profile-picture.png';
    }

    private get isDefaultImage(): boolean {
        return this.imagePath.startsWith('/assets');
    }

    private get isNewImageUploaded(): boolean {
        return this.imageType != undefined;
    }

    public abstract changeImage(): void;
}