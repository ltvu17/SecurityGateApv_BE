using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityGateApv.Infras.Migrations
{
    /// <inheritdoc />
    public partial class Update_VehicleSession_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleSessions_Gates_GateId",
                table: "VehicleSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleSessions_Gates_GateId1",
                table: "VehicleSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleSessions_Users_UserId",
                table: "VehicleSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleSessions_Users_UserId1",
                table: "VehicleSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleSessions_Users_UserId2",
                table: "VehicleSessions");

            migrationBuilder.DropIndex(
                name: "IX_VehicleSessions_GateId",
                table: "VehicleSessions");

            migrationBuilder.DropIndex(
                name: "IX_VehicleSessions_GateId1",
                table: "VehicleSessions");

            migrationBuilder.DropIndex(
                name: "IX_VehicleSessions_UserId",
                table: "VehicleSessions");

            migrationBuilder.DropIndex(
                name: "IX_VehicleSessions_UserId1",
                table: "VehicleSessions");

            migrationBuilder.DropIndex(
                name: "IX_VehicleSessions_UserId2",
                table: "VehicleSessions");

            migrationBuilder.DropColumn(
                name: "GateId",
                table: "VehicleSessions");

            migrationBuilder.DropColumn(
                name: "GateId1",
                table: "VehicleSessions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "VehicleSessions");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "VehicleSessions");

            migrationBuilder.DropColumn(
                name: "UserId2",
                table: "VehicleSessions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GateId",
                table: "VehicleSessions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GateId1",
                table: "VehicleSessions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "VehicleSessions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "VehicleSessions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId2",
                table: "VehicleSessions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSessions_GateId",
                table: "VehicleSessions",
                column: "GateId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSessions_GateId1",
                table: "VehicleSessions",
                column: "GateId1");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSessions_UserId",
                table: "VehicleSessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSessions_UserId1",
                table: "VehicleSessions",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSessions_UserId2",
                table: "VehicleSessions",
                column: "UserId2");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleSessions_Gates_GateId",
                table: "VehicleSessions",
                column: "GateId",
                principalTable: "Gates",
                principalColumn: "GateId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleSessions_Gates_GateId1",
                table: "VehicleSessions",
                column: "GateId1",
                principalTable: "Gates",
                principalColumn: "GateId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleSessions_Users_UserId",
                table: "VehicleSessions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleSessions_Users_UserId1",
                table: "VehicleSessions",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleSessions_Users_UserId2",
                table: "VehicleSessions",
                column: "UserId2",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
