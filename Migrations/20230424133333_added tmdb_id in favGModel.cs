using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace filmsystemet.Migrations
{
    public partial class addedtmdb_idinfavGModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TmdbId",
                table: "FavouriteGenres",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TmdbId",
                table: "FavouriteGenres");
        }
    }
}
