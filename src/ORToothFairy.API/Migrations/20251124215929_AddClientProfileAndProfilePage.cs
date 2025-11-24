using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ORToothFairy.API.Migrations
{
    /// <inheritdoc />
    public partial class AddClientProfileAndProfilePage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfilePages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    StockPhotoUrl = table.Column<string>(type: "text", nullable: false),
                    CardColor = table.Column<string>(type: "text", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Headline = table.Column<string>(type: "text", nullable: false),
                    ExpandedDescription = table.Column<string>(type: "text", nullable: false),
                    CardColor = table.Column<string>(type: "text", nullable: false),
                    HighlightColor = table.Column<string>(type: "text", nullable: false),
                    DefaultSearchType = table.Column<string>(type: "text", nullable: false),
                    ShowRadiusOption = table.Column<bool>(type: "boolean", nullable: false),
                    DefaultRadiusMiles = table.Column<int>(type: "integer", nullable: false),
                    PageCategory = table.Column<string>(type: "text", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UserStoryIds = table.Column<string>(type: "text", nullable: false),
                    ProfilePageId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientProfiles_ProfilePages_ProfilePageId",
                        column: x => x.ProfilePageId,
                        principalTable: "ProfilePages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientProfiles_IsActive",
                table: "ClientProfiles",
                column: "IsActive");

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

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePages_DisplayOrder",
                table: "ProfilePages",
                column: "DisplayOrder");

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePages_IsActive",
                table: "ProfilePages",
                column: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientProfiles");

            migrationBuilder.DropTable(
                name: "ProfilePages");
        }
    }
}
