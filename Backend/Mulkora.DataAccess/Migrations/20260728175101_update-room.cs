using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mulkora.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateroom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BathCound",
                table: "Rooms",
                newName: "BathCount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BathCount",
                table: "Rooms",
                newName: "BathCound");
        }
    }
}
