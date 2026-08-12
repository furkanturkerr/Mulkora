using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mulkora.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatepropertyfeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeatureProperty",
                columns: table => new
                {
                    FeaturesFeatureId = table.Column<int>(type: "int", nullable: false),
                    PropertiesPropertyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureProperty", x => new { x.FeaturesFeatureId, x.PropertiesPropertyId });
                    table.ForeignKey(
                        name: "FK_FeatureProperty_Features_FeaturesFeatureId",
                        column: x => x.FeaturesFeatureId,
                        principalTable: "Features",
                        principalColumn: "FeatureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureProperty_Properties_PropertiesPropertyId",
                        column: x => x.PropertiesPropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureProperty_PropertiesPropertyId",
                table: "FeatureProperty",
                column: "PropertiesPropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureProperty");
        }
    }
}
