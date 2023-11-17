using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelDatabase.Migrations
{
    /// <inheritdoc />
    public partial class RelationsBetweenTablesFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Capital_CapitalId",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Capital_CapitalId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Trip_CapitalId",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "City",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CapitalId",
                table: "Trip");

            migrationBuilder.RenameColumn(
                name: "CapitalId",
                table: "User",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_User_CapitalId",
                table: "User",
                newName: "IX_User_CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_ArrivalId",
                table: "Trip",
                column: "ArrivalId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_DepartureId",
                table: "Trip",
                column: "DepartureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Capital_ArrivalId",
                table: "Trip",
                column: "ArrivalId",
                principalTable: "Capital",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Capital_DepartureId",
                table: "Trip",
                column: "DepartureId",
                principalTable: "Capital",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Capital_CityId",
                table: "User",
                column: "CityId",
                principalTable: "Capital",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Capital_ArrivalId",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Capital_DepartureId",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Capital_CityId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Trip_ArrivalId",
                table: "Trip");

            migrationBuilder.DropIndex(
                name: "IX_Trip_DepartureId",
                table: "Trip");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "User",
                newName: "CapitalId");

            migrationBuilder.RenameIndex(
                name: "IX_User_CityId",
                table: "User",
                newName: "IX_User_CapitalId");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "User",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CapitalId",
                table: "Trip",
                type: "INTEGER",
                nullable: true);

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
    }
}
