import { Gender } from "../enums/Gender";

export class BusinessCard {
    public id?: number;
    public name?: string;
    public gender?: Gender;
    public birthOfDate?: Date;
    public email?: string;
    public phone?: string;
    public imageId?: number;
    public address?: string;
}