import { Component, ElementRef, inject, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { BusinessCardService } from '../../../services/business-card.service';
import { BusinessCard } from '../../../models/business-card.model';
import { BusinessCardFilter } from '../../../dtos/business-card-filter.dto';
import { Result } from '../../../models/result';
import { OperationStatus } from '../../../enums/OperationStatus';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { DeleteDialogComponent } from './delete-dialog/delete-dialog.component';
import { EditDialogComponent } from './business-card-dialog/edit-dialog/edit-dialog.component';
import { AddDialogComponent } from './business-card-dialog/add-dialog/add-dialog.component';

@Component({
  selector: 'app-business-card',
  templateUrl: './business-card.component.html',
  styleUrl: './business-card.component.scss',
})
export class BusinessCardComponent {
  public readonly filterForm = new FormGroup({
    name: new FormControl(null),
    fromBirthDate: new FormControl(null),
    toBirthDate: new FormControl(null),
    phone: new FormControl(null),
    gender: new FormControl(null),
    email: new FormControl(null)
  });

  public dataSource: Array<BusinessCard> = [];
  public columnsToDisplay: Array<string> = ['name', 'birthOfDate', 'phone', 'gender', 'email', 'actions'];
  public selectedGender: string | null = null;

  @ViewChild('filterStartBirthDate') filterStartBirthDate: ElementRef | undefined;
  @ViewChild('filterToBirthDate') filterToBirthDate: ElementRef | undefined;
  
  readonly dialog = inject(MatDialog);

  constructor(private businessService: BusinessCardService) {
    this.getAll(new BusinessCardFilter());
  }

  public filter(): void {
    const filter: BusinessCardFilter = this.getFilters();

    this.getAll(filter);
  }

  public clearFilter(): void {
    this.filterForm.reset();

    this.getAll(new BusinessCardFilter());
  }

  private getAll(filter: BusinessCardFilter) {
    this.businessService.getAll(filter).subscribe(result => {
      const mappedResult: Result<Array<BusinessCard>> = result as Result<Array<BusinessCard>>;
      
      if (mappedResult.status == OperationStatus.Success) {
        this.dataSource = mappedResult.data!;
      }
    });
  }

  public openDialog(type: string, element?: BusinessCard): void {
    switch (type) {
      case 'add':
        this.add();
        break;

      case 'edit':
        this.edit(element!);
        break;

      case 'delete':
        this.delete(element!);
        break;
    }
  }

  public export(type: string): void {
    switch (type) {
      case 'CSV':
        this.exportCSV();
        break;

      case 'XML':
        this.exportXML();
        break;
    }
  }

  private add(): void {
    const dialogRef: MatDialogRef<AddDialogComponent, any> = this.dialog.open(AddDialogComponent, {
      data: new BusinessCard(),
      width: '600px'
    });

    dialogRef.afterClosed().subscribe((result: Result<BusinessCard> | undefined) => {
      if (result != undefined && result.status == OperationStatus.Success) {
        this.dataSource = [...this.dataSource, result.data!];
      }
    });
  }

  private edit(element: BusinessCard): void {

    this.businessService.getById(element.id!).subscribe(result => {
      const mappedResult: Result<BusinessCard> = result as Result<BusinessCard>;
      
      const dialogRef: MatDialogRef<EditDialogComponent, any> = this.dialog.open(EditDialogComponent, {
        data: mappedResult.data,
        width: '600px'
      });

      dialogRef.afterClosed().subscribe((result: Result<BusinessCard> | undefined) => {
        if (result != undefined && result.status == OperationStatus.Success) {
            this.dataSource = this.dataSource.map(item => (item.id == element.id ? result.data : item)) as Array<BusinessCard>;
        }
      })
    });
 }

  private delete(element: BusinessCard): void {
    const dialogRef: MatDialogRef<DeleteDialogComponent, any> = this.dialog.open(DeleteDialogComponent, {
      data: element,
    });

    dialogRef.afterClosed().subscribe((isDeleted: boolean) => {
      if (isDeleted) {
        this.dataSource = this.dataSource.filter(card => card.id != element.id);
      }
    });
  }

  private exportCSV(): void {
    const filter: BusinessCardFilter = this.getFilters();

    this.businessService.exportCSV(filter).subscribe((result: Blob) => {
      this.downloadBlob(result, 'Apolo_Business_Cards.csv');
    });
  }

  private exportXML(): void {
    const filter: BusinessCardFilter = this.getFilters();

    this.businessService.exportXML(filter).subscribe((result: Blob) => {
      this.downloadBlob(result, 'Apolo_Business_Cards.xml')
    });
  }

  private getFilters(): BusinessCardFilter {
    const filterParameters: BusinessCardFilter = this.filterForm.value as BusinessCardFilter;

    filterParameters.gender = (this.selectedGender) ? parseInt(this.selectedGender) : null;
    filterParameters.fromBirthDate = (this.filterStartBirthDate?.nativeElement.value) ? this.filterStartBirthDate?.nativeElement.value : null;
    filterParameters.toBirthDate = (this.filterToBirthDate?.nativeElement.value) ? this.filterToBirthDate?.nativeElement.value : null;

    return filterParameters;
  }

  private downloadBlob(blob: Blob, filename: string): void {
    const url = window.URL.createObjectURL(blob);

    const anchor = document.createElement('a');
    anchor.style.display = 'none';
    anchor.href = url;
    anchor.download = filename;

    document.body.appendChild(anchor);
    anchor.click();
    document.body.removeChild(anchor);

    window.URL.revokeObjectURL(url);
  }
}
