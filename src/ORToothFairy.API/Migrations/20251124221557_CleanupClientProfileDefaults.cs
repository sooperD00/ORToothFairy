using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ORToothFairy.API.Migrations
{
    /// <inheritdoc />
    public partial class CleanupClientProfileDefaults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClientProfiles_PageCategory",
                table: "ClientProfiles");

            migrationBuilder.DropIndex(
                name: "IX_ClientProfiles_PageCategory_DisplayOrder",
                table: "ClientProfiles");

            migrationBuilder.DropIndex(
                name: "IX_ClientProfiles_ProfilePageId",
                table: "ClientProfiles");

            migrationBuilder.DropColumn(
                name: "PageCategory",
                table: "ClientProfiles");

            migrationBuilder.CreateIndex(
                name: "IX_ClientProfiles_ProfilePageId_DisplayOrder",
                table: "ClientProfiles",
                columns: new[] { "ProfilePageId", "DisplayOrder" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClientProfiles_ProfilePageId_DisplayOrder",
                table: "ClientProfiles");

            migrationBuilder.AddColumn<string>(
                name: "PageCategory",
                table: "ClientProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ClientProfiles_PageCategory",
                table: "ClientProfiles",
                column: "PageCategory");

            migrationBuilder.CreateIndex(
                name: "IX_ClientProfiles_PageCategory_DisplayOrder",
                table: "ClientProfiles",
                columns: new[] { "PageCategory", "DisplayOrder" });

            migrationBuilder.CreateIndex(
                name: "IX_ClientProfiles_ProfilePageId",
                table: "ClientProfiles",
                column: "ProfilePageId");
        }
    }
}
