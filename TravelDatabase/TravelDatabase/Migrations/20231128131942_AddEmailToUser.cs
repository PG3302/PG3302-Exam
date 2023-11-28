using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelDatabase.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "IX_User_CityId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "DepartureId",
                table: "Trip",
                newName: "DepartureCapitalId");

            migrationBuilder.RenameColumn(
                name: "ArrivalId",
                table: "Trip",
                newName: "ArrivalCapitalId");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_DepartureId",
                table: "Trip",
                newName: "IX_Trip_DepartureCapitalId");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_ArrivalId",
                table: "Trip",
                newName: "IX_Trip_ArrivalCapitalId");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "User",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Capital_ArrivalCapitalId",
                table: "Trip",
                column: "ArrivalCapitalId",
                principalTable: "Capital",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Capital_DepartureCapitalId",
                table: "Trip",
                column: "DepartureCapitalId",
                principalTable: "Capital",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Capital_ArrivalCapitalId",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Capital_DepartureCapitalId",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "DepartureCapitalId",
                table: "Trip",
                newName: "DepartureId");

            migrationBuilder.RenameColumn(
                name: "ArrivalCapitalId",
                table: "Trip",
                newName: "ArrivalId");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_DepartureCapitalId",
                table: "Trip",
                newName: "IX_Trip_DepartureId");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_ArrivalCapitalId",
                table: "Trip",
                newName: "IX_Trip_ArrivalId");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "User",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_User_CityId",
                table: "User",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Capital_ArrivalId",
                table: "Trip",
                column: "ArrivalId",
                principalTable: "Capital",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Capital_DepartureId",
                table: "Trip",
                column: "DepartureId",
                principalTable: "Capital",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Capital_CityId",
                table: "User",
                column: "CityId",
                principalTable: "Capital",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
