using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mulkora.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateappointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Agents_AgentId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_AppUserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Properties_PropertyId",
                table: "Appointments");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Agents_AgentId",
                table: "Appointments",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "AgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_AppUserId",
                table: "Appointments",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Properties_PropertyId",
                table: "Appointments",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "PropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Agents_AgentId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_AppUserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Properties_PropertyId",
                table: "Appointments");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Agents_AgentId",
                table: "Appointments",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "AgentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_AppUserId",
                table: "Appointments",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Properties_PropertyId",
                table: "Appointments",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "PropertyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
