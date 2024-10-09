import { Gender } from "../enums/Gender";

export class BusinessCard {
    public id?: string;
    public name?: string;
    public gender?: Gender;
    public birthOfDate?: Date;
    public email?: string;
    public phone?: string;
    public photo?: string;
    public address?: string;
}