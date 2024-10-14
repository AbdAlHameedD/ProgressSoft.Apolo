using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgressSoft.Apolo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddImageMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PrimaryKey_ BusinessCards",
                schema: "dbo",
                table: "BusinessCards");

            migrationBuilder.DropColumn(
                name: "Photo",
                schema: "dbo",
                table: "BusinessCards");

            migrationBuilder.EnsureSchema(
                name: "asset");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                schema: "dbo",
                table: "BusinessCards",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AddPrimaryKey(
                name: "PrimaryKey_BusinessCards",
                schema: "dbo",
                table: "BusinessCards",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Images",
                schema: "asset",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EncodedImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCards_ImageId",
                schema: "dbo",
                table: "BusinessCards",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_BusinessCard",
                schema: "dbo",
                table: "BusinessCards",
                column: "ImageId",
                principalSchema: "asset",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_BusinessCard",
                schema: "dbo",
                table: "BusinessCards");

            migrationBuilder.DropTable(
                name: "Images",
                schema: "asset");

            migrationBuilder.DropPrimaryKey(
                name: "PrimaryKey_BusinessCards",
                schema: "dbo",
                table: "BusinessCards");

            migrationBuilder.DropIndex(
                name: "IX_BusinessCards_ImageId",
                schema: "dbo",
                table: "BusinessCards");

            migrationBuilder.DropColumn(
                name: "ImageId",
                schema: "dbo",
                table: "BusinessCards");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                schema: "dbo",
                table: "BusinessCards",
                type: "varbinary(max)",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 7);

            migrationBuilder.AddPrimaryKey(
                name: "PrimaryKey_ BusinessCards",
                schema: "dbo",
                table: "BusinessCards",
                column: "Id");
        }
    }
}
