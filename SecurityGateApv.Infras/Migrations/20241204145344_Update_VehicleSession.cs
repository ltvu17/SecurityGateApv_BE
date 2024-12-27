using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityGateApv.Infras.Migrations
{
    /// <inheritdoc />
    public partial class Update_VehicleSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleSessions_Gates_GateInId",
                table: "VehicleSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleSessions_Gates_GateOutId",
                table: "VehicleSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleSessions_Users_SecurityInId",
                table: "VehicleSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleSessions_Users_SecurityOutId",
                table: "VehicleSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleSessions_VisitDetails_VisitDetailId",
                table: "VehicleSessions");

            migrationBuilder.DropIndex(
                name: "IX_VehicleSessions_GateInId",
                table: "VehicleSessions");

            migrationBuilder.DropIndex(
                name: "IX_VehicleSessions_SecurityInId",
                table: "VehicleSessions");

            migrationBuilder.DropIndex(
                name: "IX_VehicleSessions_VisitDetailId",
                table: "VehicleSessions");

            migrationBuilder.DropColumn(
                name: "CheckinTime",
                table: "VehicleSessions");

            migrationBuilder.DropColumn(
                name: "CheckoutTime",
                table: "VehicleSessions");

            migrationBuilder.DropColumn(
                name: "GateInId",
                table: "VehicleSessions");

            migrationBuilder.DropColumn(
                name: "SecurityInId",
                table: "VehicleSessions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "VehicleSessions");

            migrationBuilder.RenameColumn(
                name: "VisitDetailId",
                table: "VehicleSessions",
                newName: "VisitorSessionId");

            migrationBuilder.RenameColumn(
                name: "SecurityOutId",
                table: "VehicleSessions",
                newName: "UserId2");

            migrationBuilder.RenameColumn(
                name: "GateOutId",
                table: "VehicleSessions",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleSessions_SecurityOutId",
                table: "VehicleSessions",
                newName: "IX_VehicleSessions_UserId2");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleSessions_GateOutId",
                table: "VehicleSessions",
                newName: "IX_VehicleSessions_UserId1");

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

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSessions_GateId",
                table: "VehicleSessions",
                column: "GateId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSessions_GateId1",
                table: "VehicleSessions",
                column: "GateId1");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSessions_VisitorSessionId",
                table: "VehicleSessions",
                column: "VisitorSessionId",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleSessions_VisitorSessions_VisitorSessionId",
                table: "VehicleSessions",
                column: "VisitorSessionId",
                principalTable: "VisitorSessions",
                principalColumn: "VisitorSessionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleSessions_Gates_GateId",
                table: "VehicleSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleSessions_Gates_GateId1",
                table: "VehicleSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleSessions_Users_UserId1",
                table: "VehicleSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleSessions_Users_UserId2",
                table: "VehicleSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleSessions_VisitorSessions_VisitorSessionId",
                table: "VehicleSessions");

            migrationBuilder.DropIndex(
                name: "IX_VehicleSessions_GateId",
                table: "VehicleSessions");

            migrationBuilder.DropIndex(
                name: "IX_VehicleSessions_GateId1",
                table: "VehicleSessions");

            migrationBuilder.DropIndex(
                name: "IX_VehicleSessions_VisitorSessionId",
                table: "VehicleSessions");

            migrationBuilder.DropColumn(
                name: "GateId",
                table: "VehicleSessions");

            migrationBuilder.DropColumn(
                name: "GateId1",
                table: "VehicleSessions");

            migrationBuilder.RenameColumn(
                name: "VisitorSessionId",
                table: "VehicleSessions",
                newName: "VisitDetailId");

            migrationBuilder.RenameColumn(
                name: "UserId2",
                table: "VehicleSessions",
                newName: "SecurityOutId");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "VehicleSessions",
                newName: "GateOutId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleSessions_UserId2",
                table: "VehicleSessions",
                newName: "IX_VehicleSessions_SecurityOutId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleSessions_UserId1",
                table: "VehicleSessions",
                newName: "IX_VehicleSessions_GateOutId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckinTime",
                table: "VehicleSessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckoutTime",
                table: "VehicleSessions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GateInId",
                table: "VehicleSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SecurityInId",
                table: "VehicleSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "VehicleSessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSessions_GateInId",
                table: "VehicleSessions",
                column: "GateInId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSessions_SecurityInId",
                table: "VehicleSessions",
                column: "SecurityInId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSessions_VisitDetailId",
                table: "VehicleSessions",
                column: "VisitDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleSessions_Gates_GateInId",
                table: "VehicleSessions",
                column: "GateInId",
                principalTable: "Gates",
                principalColumn: "GateId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleSessions_Gates_GateOutId",
                table: "VehicleSessions",
                column: "GateOutId",
                principalTable: "Gates",
                principalColumn: "GateId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleSessions_Users_SecurityInId",
                table: "VehicleSessions",
                column: "SecurityInId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleSessions_Users_SecurityOutId",
                table: "VehicleSessions",
                column: "SecurityOutId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleSessions_VisitDetails_VisitDetailId",
                table: "VehicleSessions",
                column: "VisitDetailId",
                principalTable: "VisitDetails",
                principalColumn: "VisitDetailId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
