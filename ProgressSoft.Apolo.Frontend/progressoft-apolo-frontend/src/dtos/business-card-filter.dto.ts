import { Gender } from "../enums/Gender";

export class BusinessCardFilter {
    public name: string | null = null;
    public gender: Gender | null = null;
    public fromBirthDate: Date | null = null;
    public toBirthDate: Date | null = null;
    public phone: string | null = null;
    public email: string | null = null;
}