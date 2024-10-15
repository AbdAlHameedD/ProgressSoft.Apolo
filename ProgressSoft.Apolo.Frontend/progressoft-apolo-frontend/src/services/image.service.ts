import { HttpClient } from "@angular/common/http";
import { Image } from "../models/image.model";
import { Observable } from "rxjs";
import { Environment } from "../constants/environment";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class ImageService {
    private readonly CONTROLLER_ROUTE: string = `${Environment.API_PROTOCOL}://${Environment.API_HOST}:${Environment.API_PORT}/api/Image`;
    private readonly ADD_FULL_ROUTE = `${this.CONTROLLER_ROUTE}/Add`;
    private readonly DELETE_FULL_ROUTE = `${this.CONTROLLER_ROUTE}/Delete`;
    private readonly GET_BY_ID_FULL_ROUTE = `${this.CONTROLLER_ROUTE}/GetById`
    
    constructor(private http: HttpClient) { }

    add(model: Image): Observable<any> {
        return this.http.post(this.ADD_FULL_ROUTE, model);
    }

    delete(id: number): Observable<any> {
        return this.http.delete(`${this.DELETE_FULL_ROUTE}/${id}`);
    }

    getById(id: number): Observable<any> {
        return this.http.get(`${this.GET_BY_ID_FULL_ROUTE}/${id}`);
    }
}