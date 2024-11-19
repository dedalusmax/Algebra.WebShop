using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Algebra.WebShop.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class productimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "FileContent",
                table: "Products",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileContent",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Products");
        }
    }
}
