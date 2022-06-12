using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBiz.Migrations
{
    public partial class FieldNameChangedInMainSliders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrnUrl",
                table: "MainSliders");

            migrationBuilder.AddColumn<string>(
                name: "BtnUrl",
                table: "MainSliders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BtnUrl",
                table: "MainSliders");

            migrationBuilder.AddColumn<string>(
                name: "BrnUrl",
                table: "MainSliders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
