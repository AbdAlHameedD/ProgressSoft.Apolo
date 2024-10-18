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
import { ExternalBusinessCard } from '../../../dtos/import-business-card.dto';
import { Gender } from '../../../enums/Gender';
import { PreviewDialogComponent } from './preview-dialog/preview-dialog.component';

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
  @ViewChild('import') import: ElementRef | undefined;

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

  public openImportDialog(): void {
    this.import?.nativeElement.click();
  }

  public onImport(event: any): void {
    const file: File = event.target.files[0];
    (<HTMLInputElement>this.import?.nativeElement).value = '';

    if (file) {
      if (file.type === 'text/csv') {
        this.importCSV(file);
      } else if (file.type === 'text/xml') {
        this.importXML(file);
      }
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

  private importCSV(file: File): void {
    const fileReader = new FileReader();

    fileReader.onload = () => {
      const csvContent: string = fileReader.result as string;
      const rows: Array<string> = csvContent.split('\n');
      const importedBusinessCards: Array<ExternalBusinessCard> = [];

      let i: number;
      for (i = 1; i < rows.length-1; i++) {
        const row: string = rows[i];
        const columns: Array<string> = row.split(',');
        
        const businessCard: ExternalBusinessCard = new ExternalBusinessCard();
        for (let i: number = 1; i < columns.length; i++) {
          switch (i) {
            case 1:
              businessCard.name = columns[i];
              break;

            case 2:
              businessCard.gender = columns[2] as unknown as Gender;
              break;

            case 3:
              businessCard.birthOfDate = columns[i];
              break;
            
            case 4:
              businessCard.email = columns[i];
              break;

            case 5:
              businessCard.phone = columns[i];
              break;

            case 6:
              businessCard.address = columns[i];
              break;

            case 7:
              businessCard.image = columns[i];
              break;

            case 8:
              businessCard.imageType = columns[i];
              break;
          }
        }

        importedBusinessCards.push(businessCard);
      }

      if (importedBusinessCards.length > 0) {
        this.openPreviewDialog(importedBusinessCards);
      } else {
        this.openErrorDialog('There is no valid data to import');
      }
    }

    fileReader.readAsText(file);
  }

  private importXML(file: File): void {
    const fileReader = new FileReader();

    fileReader.onload = () => {
      const xmlText: string = fileReader.result as string;
          
      const parser: DOMParser = new DOMParser();
      const xmlDocument: Document = parser.parseFromString(xmlText, 'text/xml');

      const xmlBusinessCards: HTMLCollectionOf<Element> = xmlDocument.getElementsByTagName('BusinessCard');
      const importedBusinessCards: Array<ExternalBusinessCard> = [];

      for (let i: number = 0; i < xmlBusinessCards.length; i++) {
        const businessCard: ExternalBusinessCard = {
          name: xmlBusinessCards[i].getAttribute('Name'),
          address: xmlBusinessCards[i].getAttribute('Address'),
          birthOfDate: xmlBusinessCards[i].getAttribute('BirthOfDate'),
          email: xmlBusinessCards[i].getAttribute('Email'),
          gender: xmlBusinessCards[i].getAttribute('Gender') as unknown as Gender,
          phone: xmlBusinessCards[i].getAttribute('Phone'),
          image: xmlBusinessCards[i].getAttribute('Image'),
          imageType: xmlBusinessCards[i].getAttribute('ImageType')
        };

        importedBusinessCards.push(businessCard);
      }

      if (importedBusinessCards.length > 0) {
        this.openPreviewDialog(importedBusinessCards);
      } else {
        this.openErrorDialog('There is no valid data to import');
      }
    }

    fileReader.readAsText(file);
  }

  private openPreviewDialog(businessCards: Array<ExternalBusinessCard>): void {
    const dialogRef: MatDialogRef<PreviewDialogComponent, any> = this.dialog.open(PreviewDialogComponent, {
      data: businessCards,
      width: '600px'
    });

    dialogRef.afterClosed().subscribe((result: Array<BusinessCard> | null) => {
      if (result && result.length > 0) {
        this.dataSource = [...this.dataSource, ...result];
      }
    });
  }

  private openErrorDialog(message: string): void {

  }
}
