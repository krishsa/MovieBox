using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MovieBox.Migrations
{
    public partial class AddGenre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "Movies",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "Movies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "NumberAvailable",
                table: "Movies",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "NumberInStock",
                table: "Movies",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Movies",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenreId",
                table: "Movies",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genre_GenreId",
                table: "Movies",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genre_GenreId",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Movies_GenreId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "NumberAvailable",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "NumberInStock",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Movies");
        }
    }
}
