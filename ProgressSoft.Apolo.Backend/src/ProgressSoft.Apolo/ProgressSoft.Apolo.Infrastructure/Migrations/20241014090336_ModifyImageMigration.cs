using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgressSoft.Apolo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyImageMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "asset",
                table: "Images",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                schema: "asset",
                table: "Images");
        }
    }
}
