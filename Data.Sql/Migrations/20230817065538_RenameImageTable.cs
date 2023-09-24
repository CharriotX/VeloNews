using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class RenameImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "NewsImage");

            migrationBuilder.AddColumn<int>(
                name: "NewsId",
                table: "NewsImage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsImage",
                table: "NewsImage",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_NewsImage_NewsId",
                table: "NewsImage",
                column: "NewsId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsImage_News_NewsId",
                table: "NewsImage",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsImage_News_NewsId",
                table: "NewsImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsImage",
                table: "NewsImage");

            migrationBuilder.DropIndex(
                name: "IX_NewsImage_NewsId",
                table: "NewsImage");

            migrationBuilder.DropColumn(
                name: "NewsId",
                table: "NewsImage");

            migrationBuilder.RenameTable(
                name: "NewsImage",
                newName: "Images");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");
        }
    }
}
