using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace banknote.Migrations
{
    /// <inheritdoc />
    public partial class Userrr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pictures_Users_UserId",
                table: "pictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pictures",
                table: "pictures");

            migrationBuilder.RenameTable(
                name: "pictures",
                newName: "Pictures");

            migrationBuilder.RenameIndex(
                name: "IX_pictures_UserId",
                table: "Pictures",
                newName: "IX_Pictures_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pictures",
                table: "Pictures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Users_UserId",
                table: "Pictures",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Users_UserId",
                table: "Pictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pictures",
                table: "Pictures");

            migrationBuilder.RenameTable(
                name: "Pictures",
                newName: "pictures");

            migrationBuilder.RenameIndex(
                name: "IX_Pictures_UserId",
                table: "pictures",
                newName: "IX_pictures_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pictures",
                table: "pictures",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_Users_UserId",
                table: "pictures",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
