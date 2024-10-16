export class Environment {
    public static readonly API_PROTOCOL: string = 'http';
    public static readonly API_HOST: string = 'localhost';
    public static readonly API_PORT: number = 5205;
    public static readonly WEBSITE_TITLE: string = 'Apolo';

    public static readonly DATE_FORMATS = {
        parse: {
          dateInput: 'LL',
        },
        display: {
          dateInput: 'YYYY-MM-DD',
          monthYearLabel: 'YYYY',
          dateA11yLabel: 'LL',
          monthYearA11yLabel: 'YYYY',
        },
      };
}