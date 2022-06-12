using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBiz.Migrations
{
    public partial class OrderFieldAddToMainSliders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "MainSliders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "MainSliders");
        }
    }
}
