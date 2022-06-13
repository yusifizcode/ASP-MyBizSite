using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBiz.Migrations
{
    public partial class PortfolioImagesTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioImage_Portfolios_PortfolioId",
                table: "PortfolioImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PortfolioImage",
                table: "PortfolioImage");

            migrationBuilder.RenameTable(
                name: "PortfolioImage",
                newName: "PortfolioImages");

            migrationBuilder.RenameIndex(
                name: "IX_PortfolioImage_PortfolioId",
                table: "PortfolioImages",
                newName: "IX_PortfolioImages_PortfolioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PortfolioImages",
                table: "PortfolioImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioImages_Portfolios_PortfolioId",
                table: "PortfolioImages",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioImages_Portfolios_PortfolioId",
                table: "PortfolioImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PortfolioImages",
                table: "PortfolioImages");

            migrationBuilder.RenameTable(
                name: "PortfolioImages",
                newName: "PortfolioImage");

            migrationBuilder.RenameIndex(
                name: "IX_PortfolioImages_PortfolioId",
                table: "PortfolioImage",
                newName: "IX_PortfolioImage_PortfolioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PortfolioImage",
                table: "PortfolioImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioImage_Portfolios_PortfolioId",
                table: "PortfolioImage",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
