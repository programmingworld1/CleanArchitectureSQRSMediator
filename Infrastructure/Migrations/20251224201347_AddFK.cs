using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArtistId",
                table: "Songs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ArtistName",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Artist",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_ArtistId",
                table: "Songs",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Artist_Name",
                table: "Artist",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Artist_ArtistId",
                table: "Songs",
                column: "ArtistId",
                principalTable: "Artist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Artist_ArtistId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_ArtistId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Artist_Name",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "ArtistName",
                table: "Songs");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Artist",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
