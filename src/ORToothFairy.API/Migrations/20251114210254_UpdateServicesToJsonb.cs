using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ORToothFairy.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateServicesToJsonb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxTravelDistanceMiles",
                table: "Practitioners",
                newName: "MaxTravelMiles");

            migrationBuilder.AlterColumn<string>(
                name: "Services",
                table: "Practitioners",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxTravelMiles",
                table: "Practitioners",
                newName: "MaxTravelDistanceMiles");

            migrationBuilder.AlterColumn<string>(
                name: "Services",
                table: "Practitioners",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "jsonb");
        }
    }
}
