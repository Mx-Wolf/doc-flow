using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DocumentDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentData",
                schema: "df",
                table: "Formulars",
                type: "nvarchar(800)",
                maxLength: 800,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Presentable_IsEnabled",
                schema: "df",
                table: "Formulars",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentData",
                schema: "df",
                table: "Formulars");

            migrationBuilder.DropColumn(
                name: "Presentable_IsEnabled",
                schema: "df",
                table: "Formulars");
        }
    }
}
