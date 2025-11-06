using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ORToothFairy.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPractitionerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BusinessName",
                table: "Practitioners",
                newName: "Website");

            migrationBuilder.AddColumn<string>(
                name: "PracticeName",
                table: "Practitioners",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Practitioners",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Practitioners",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PracticeName",
                table: "Practitioners");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Practitioners");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Practitioners");

            migrationBuilder.RenameColumn(
                name: "Website",
                table: "Practitioners",
                newName: "BusinessName");
        }
    }
}
