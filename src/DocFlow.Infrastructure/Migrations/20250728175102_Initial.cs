using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "df");

            migrationBuilder.CreateTable(
                name: "Formulars",
                schema: "df",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Presentable_Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Presentable_Code = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Presentable_Color = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Presentable_SequenceNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formulars", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Formulars",
                schema: "df");
        }
    }
}
