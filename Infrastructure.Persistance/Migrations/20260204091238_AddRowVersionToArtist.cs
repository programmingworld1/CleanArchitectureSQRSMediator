using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddRowVersionToArtist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArtistName",
                table: "Songs");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Artist",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Artist");

            migrationBuilder.AddColumn<string>(
                name: "ArtistName",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
