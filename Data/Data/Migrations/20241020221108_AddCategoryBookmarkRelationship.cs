using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddCategoryBookmarkRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_Categories_CategoryId",
                table: "Bookmark");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_Categories_CategoryId",
                table: "Bookmark",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_Categories_CategoryId",
                table: "Bookmark");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_Categories_CategoryId",
                table: "Bookmark",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
