# ProgressSoft Apolo

Technologies Used:
   - Angular v18.2.7 (Latest).
   - .NET 8 (Latest).
   - SQL Server.

* Clean Architecture used to build backend with four layers (Domain -> Application -> Infrastructure, Web API).
* Code first approach used using Entity Framework Core 8 (EF Core 8).

* Application consists of two pages (Business Card Page, and Not Found Page)
  - /home/businesscard
 
1) Install Setup Instructions:
   - Set ProgressSoft.Apolor.API as Startup Up Project.
   - Make sure the backend url (protocol, domain, port, etc) is same in angular project src\constants\environment.ts file.
   - Restore ProgressSoft.Apolo.bak database (Don't change database name).
   - Run "npm install" in angular project using cmd.
     
 2) Run the application:
   - Run backend.
   - Run "ng s -o" in angular project using cmd.

* Note that QR Code reader not implemented.
