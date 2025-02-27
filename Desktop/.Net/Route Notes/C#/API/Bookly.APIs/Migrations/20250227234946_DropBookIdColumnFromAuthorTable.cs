using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookly.APIs.Migrations
{
    /// <inheritdoc />
    public partial class DropBookIdColumnFromAuthorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Authors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
