import { Gender } from "../enums/Gender";

export class ExternalBusinessCard {
    public name: string | null = null;
    public gender: Gender | null = null;
    public birthOfDate: Date | string | null = null;
    public phone: string | null = null;
    public email: string | null = null;
    public address: string | null = null;
    public image?: string | null = null;
    public imageType?: string | null = null;
}