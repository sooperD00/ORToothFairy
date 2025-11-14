using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ORToothFairy.API.Migrations
{
    /// <inheritdoc />
    public partial class AddContactPreferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AcceptsCalls",
                table: "Practitioners",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AcceptsTexts",
                table: "Practitioners",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptsCalls",
                table: "Practitioners");

            migrationBuilder.DropColumn(
                name: "AcceptsTexts",
                table: "Practitioners");
        }
    }
}
