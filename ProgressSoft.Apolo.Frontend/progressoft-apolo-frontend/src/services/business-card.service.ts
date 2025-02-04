import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { Environment } from "../constants/environment";
import { BusinessCard } from "../models/business-card.model";
import { BusinessCardFilter } from "../dtos/business-card-filter.dto";
import { ExternalBusinessCard } from "../dtos/import-business-card.dto";
import { Gender } from "../enums/Gender";

@Injectable({
    providedIn: 'root'
})
export class BusinessCardService {
    private readonly CONTROLLER_ROUTE: string = `${Environment.API_PROTOCOL}://${Environment.API_HOST}:${Environment.API_PORT}/api/BusinessCard`;
    private readonly GET_ALL_API_FULL_ROUTE = `${this.CONTROLLER_ROUTE}/Get`;
    private readonly ADD_FULL_ROUTE = `${this.CONTROLLER_ROUTE}/Add`;
    private readonly DELETE_FULL_ROUTE = `${this.CONTROLLER_ROUTE}/Delete`;
    private readonly EDIT_FULL_ROUTE = `${this.CONTROLLER_ROUTE}/Edit`;
    private readonly GET_BY_ID_FULL_ROUTE = `${this.CONTROLLER_ROUTE}/GetById`;
    private readonly EXPORT_CSV_FULL_ROUTE = `${this.CONTROLLER_ROUTE}/ExportCSV`;
    private readonly EXPORT_XML_FULL_ROUTE = `${this.CONTROLLER_ROUTE}/ExportXML`;
    private readonly ADD_BULK_FULL_ROUTE = `${this.CONTROLLER_ROUTE}/AddBulk`;

    constructor(private http: HttpClient) { }

    getAll(filter: BusinessCardFilter): Observable<any> {
       return this.http.post(this.GET_ALL_API_FULL_ROUTE, filter);
    }

    add(businessCard: BusinessCard): Observable<any> {
        return this.http.post(this.ADD_FULL_ROUTE, {
            name: businessCard.name,
            gender: parseInt(businessCard.gender!.toString()),
            email: businessCard.email,
            phone: businessCard.phone,
            imageId: businessCard.imageId,
            address: businessCard.address,
            birthOfDate: businessCard.birthOfDate!.toISOString().split('T')[0]
        });
    }

    delete(id: number): Observable<any> {
        return this.http.delete(`${this.DELETE_FULL_ROUTE}/${id}`);
    }

    edit(businessCard: BusinessCard): Observable<any> {
        return this.http.put(this.EDIT_FULL_ROUTE, {
            id: businessCard.id,
            name: businessCard.name,
            gender: parseInt(businessCard.gender!.toString()),
            email: businessCard.email,
            phone: businessCard.phone,
            imageId: businessCard.imageId,
            address: businessCard.address,
            birthOfDate: (businessCard.birthOfDate instanceof Date) ? businessCard.birthOfDate!.toISOString().split('T')[0] : businessCard.birthOfDate,
        });
    }

    getById(id: number): Observable<any> {
        return this.http.get(`${this.GET_BY_ID_FULL_ROUTE}/${id}`);
    }

    exportCSV(filter: BusinessCardFilter): Observable<any> {
        return this.http.post(this.EXPORT_CSV_FULL_ROUTE, filter, { responseType: 'blob' });
    }

    exportXML(filter: BusinessCardFilter): Observable<any> {
        return this.http.post(this.EXPORT_XML_FULL_ROUTE, filter, { responseType: 'blob' });
    }

    addBulk(businessCards: Array<ExternalBusinessCard>): Observable<any> {
        for (let card of businessCards) {
            card.gender = (card.gender!.toString() == 'Male') ? Gender.Male : Gender.Female; 
            card.birthOfDate = (typeof(card.birthOfDate) == 'string') ? card.birthOfDate : (<Date>card.birthOfDate)!.toISOString().split('T')[0];
        }

        return this.http.post(this.ADD_BULK_FULL_ROUTE, businessCards);
    }
}