using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mulkora.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class propertpropertyimage1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCover",
                table: "PropertyImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCover",
                table: "PropertyImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
