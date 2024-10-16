import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BusinessCard } from '../../../../models/business-card.model';
import { BusinessCardService } from '../../../../services/business-card.service';
import { Result } from '../../../../models/result';
import { OperationStatus } from '../../../../enums/OperationStatus';

@Component({
  selector: 'app-delete-dialog',
  templateUrl: './delete-dialog.component.html',
  styleUrl: './delete-dialog.component.scss'
})
export class DeleteDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: BusinessCard,
    private businessCardService: BusinessCardService,
    private dialogRef: MatDialogRef<DeleteDialogComponent>) {  }

  public delete(): void {
    this.businessCardService.delete(this.data.id!).subscribe(result => {
      const mappedResult: Result<BusinessCard> = result as Result<BusinessCard>;

      if (mappedResult.status = OperationStatus.Success) {
        this.dialogRef.close(true);
      } else {
        this.dialogRef.close(false);
      }
    });
  }

  public cancel(): void {
    this.dialogRef.close(false);
  }
}
