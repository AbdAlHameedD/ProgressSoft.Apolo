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

  @ViewChild('filterName') filterName: ElementRef | undefined;
  @ViewChild('filterStartBirthDate') filterStartBirthDate: ElementRef | undefined;
  @ViewChild('filterToBirthDate') filterToBirthDate: ElementRef | undefined;
  @ViewChild('filterPhone') filterPhone: ElementRef | undefined;
  @ViewChild('filterGender') filterGender: ElementRef | undefined;
  @ViewChild('filterEmail') filterEmail: ElementRef | undefined;
  
  readonly dialog = inject(MatDialog);

  constructor(private businessService: BusinessCardService) {
    this.getAll(new BusinessCardFilter());
  }

  public filter(): void {
    const filterParameters: BusinessCardFilter = new BusinessCardFilter();
    filterParameters.name = (this.filterName?.nativeElement.value) ? this.filterName?.nativeElement.value : null;
    filterParameters.phone = (this.filterPhone?.nativeElement.value) ? this.filterPhone?.nativeElement.value : null;
    filterParameters.email = (this.filterEmail?.nativeElement.value) ? this.filterEmail?.nativeElement.value : null;
    filterParameters.gender = (this.selectedGender) ? parseInt(this.selectedGender) : null;
    filterParameters.fromBirthDate = (this.filterStartBirthDate?.nativeElement.value) ? this.filterStartBirthDate?.nativeElement.value : null;
    filterParameters.toBirthDate = (this.filterToBirthDate?.nativeElement.value) ? this.filterToBirthDate?.nativeElement.value : null;

    this.getAll(filterParameters);
  }

  public clearFilter(): void {
    this.filterName!.nativeElement.value = '';
    this.filterPhone!.nativeElement.value = '';
    this.filterEmail!.nativeElement.value = '';
    this.selectedGender = null;
    this.filterStartBirthDate!.nativeElement.value = '';
    this.filterToBirthDate!.nativeElement.value = '';

    this.getAll(new BusinessCardFilter());
  }

  public getAll(filter: BusinessCardFilter) {
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
}
