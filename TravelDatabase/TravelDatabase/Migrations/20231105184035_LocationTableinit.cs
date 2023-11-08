using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelDatabase.Migrations
{
    /// <inheritdoc />
    public partial class LocationTableinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CapitalId",
                table: "User",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CapitalId",
                table: "Trip",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Capital",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CapitalName = table.Column<string>(type: "TEXT", nullable: true),
                    Continent = table.Column<string>(type: "TEXT", nullable: true),
                    Longitude = table.Column<int>(type: "INTEGER", nullable: false),
                    Latitude = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capital", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_CapitalId",
                table: "User",
                column: "CapitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_CapitalId",
                table: "Trip",
                column: "CapitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Capital_CapitalId",
                table: "Trip",
                column: "CapitalId",
                principalTable: "Capital",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Capital_CapitalId",
                table: "User",
                column: "CapitalId",
                principalTable: "Capital",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Capital_CapitalId",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Capital_CapitalId",
                table: "User");

            migrationBuilder.DropTable(
                name: "Capital");

            migrationBuilder.DropIndex(
                name: "IX_User_CapitalId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Trip_CapitalId",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "CapitalId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CapitalId",
                table: "Trip");
        }
    }
}
