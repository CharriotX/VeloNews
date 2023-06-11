using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class AddRelateNewsToNewsCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "News",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_News_CategoryId",
                table: "News",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_NewsCategories_CategoryId",
                table: "News",
                column: "CategoryId",
                principalTable: "NewsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_NewsCategories_CategoryId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_CategoryId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "News");
        }
    }
}
