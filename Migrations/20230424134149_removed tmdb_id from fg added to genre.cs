using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace filmsystemet.Migrations
{
    public partial class removedtmdb_idfromfgaddedtogenre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TmdbId",
                table: "FavouriteGenres");

            migrationBuilder.AddColumn<int>(
                name: "TmdbId",
                table: "Genres",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TmdbId",
                table: "Genres");

            migrationBuilder.AddColumn<int>(
                name: "TmdbId",
                table: "FavouriteGenres",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
