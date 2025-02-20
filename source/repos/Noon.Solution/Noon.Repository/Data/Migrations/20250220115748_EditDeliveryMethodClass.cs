using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Noon.Repository.Migrations
{
    public partial class EditDeliveryMethodClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryTime",
                table: "DeliveryMethods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryTime",
                table: "DeliveryMethods");
        }
    }
}
