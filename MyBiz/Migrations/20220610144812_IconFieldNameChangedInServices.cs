using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBiz.Migrations
{
    public partial class IconFieldNameChangedInServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "icon",
                table: "Services",
                newName: "Icon");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Icon",
                table: "Services",
                newName: "icon");
        }
    }
}
