using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTNest.Data.Migrations
{
    /// <inheritdoc />
    public partial class lkdkaldkla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ingredient",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Warning",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ingredient",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Warning",
                table: "Product");
        }
    }
}
