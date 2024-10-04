using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgressSoft.Apolo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "BusinessCards",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Gender = table.Column<byte>(type: "tinyint", nullable: false),
                    BirthOfDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "varchar(320)", unicode: false, nullable: false),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_ BusinessCards", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "Unique_BusinessCard_Email",
                schema: "dbo",
                table: "BusinessCards",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Unique_BusinessCard_Name",
                schema: "dbo",
                table: "BusinessCards",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Unique_BusinessCard_Phone",
                schema: "dbo",
                table: "BusinessCards",
                column: "Phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessCards",
                schema: "dbo");
        }
    }
}
