import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { Environment } from "../constants/environment";
import { BusinessCard } from "../models/business-card.model";
import { BusinessCardFilter } from "../dtos/business-card-filter.dto";

@Injectable({
    providedIn: 'root'
})
export class BusinessCardService {
    private readonly CONTROLLER_ROUTE: string = `${Environment.API_PROTOCOL}://${Environment.API_HOST}:${Environment.API_PORT}/api/BusinessCard`;
    private readonly GET_ALL_API_FULL_ROUTE = `${this.CONTROLLER_ROUTE}/Get`;
    private readonly ADD_FULL_ROUTE = `${this.CONTROLLER_ROUTE}/Add`;
    private readonly DELETE_FULL_ROUTE = `${this.CONTROLLER_ROUTE}/Delete`;
    private readonly EDIT_FULL_ROUTE = `${this.CONTROLLER_ROUTE}/Edit`;

    constructor(private http: HttpClient) { }

    getAll(filter: BusinessCardFilter): Observable<any> {
       return this.http.post(this.GET_ALL_API_FULL_ROUTE, filter);
    }

    add(businessCard: BusinessCard): Observable<any> {
        return this.http.post(this.ADD_FULL_ROUTE, businessCard);
    }

    delete(id: number): Observable<any> {
        return this.http.delete(`${this.DELETE_FULL_ROUTE}/${id}`);
    }

    edit(businessCard: BusinessCard): Observable<any> {
        return this.http.put(this.EDIT_FULL_ROUTE, businessCard);
    }
}