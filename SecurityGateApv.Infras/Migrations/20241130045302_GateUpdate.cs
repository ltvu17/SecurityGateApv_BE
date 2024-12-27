using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityGateApv.Infras.Migrations
{
    /// <inheritdoc />
    public partial class GateUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cameras",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Gates",
                keyColumn: "GateId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "CaptureURL",
                table: "Cameras");

            migrationBuilder.RenameColumn(
                name: "StreamURL",
                table: "Cameras",
                newName: "CameraURL");

            migrationBuilder.UpdateData(
                table: "CameraTypes",
                keyColumn: "CameraTypeId",
                keyValue: 1,
                columns: new[] { "CameraTypeName", "Description" },
                values: new object[] { "CheckIn_Shoe", "Camera chụp giày khi checkin." });

            migrationBuilder.UpdateData(
                table: "CameraTypes",
                keyColumn: "CameraTypeId",
                keyValue: 2,
                columns: new[] { "CameraTypeName", "Description" },
                values: new object[] { "CheckIn_Body", "Camera chụp toàn thân khi checkin." });

            migrationBuilder.InsertData(
                table: "CameraTypes",
                columns: new[] { "CameraTypeId", "CameraTypeName", "Description" },
                values: new object[] { 3, "CheckOut_Shoe", "Camera chụp giày khi Checkout." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CameraTypes",
                keyColumn: "CameraTypeId",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "CameraURL",
                table: "Cameras",
                newName: "StreamURL");

            migrationBuilder.AddColumn<string>(
                name: "CaptureURL",
                table: "Cameras",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "CameraTypes",
                keyColumn: "CameraTypeId",
                keyValue: 1,
                columns: new[] { "CameraTypeName", "Description" },
                values: new object[] { "Visitor_Body", "Camera chụp toàn thân." });

            migrationBuilder.UpdateData(
                table: "CameraTypes",
                keyColumn: "CameraTypeId",
                keyValue: 2,
                columns: new[] { "CameraTypeName", "Description" },
                values: new object[] { "Visitor_Shoe", "Camera chụp giày." });

            migrationBuilder.InsertData(
                table: "Gates",
                columns: new[] { "GateId", "CreateDate", "Description", "GateName", "Status" },
                values: new object[] { 1, new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cổng A", "Cổng A", true });

            migrationBuilder.InsertData(
                table: "Cameras",
                columns: new[] { "Id", "CameraTypeId", "CaptureURL", "Description", "GateId", "Status", "StreamURL" },
                values: new object[] { 1, 1, "https://security-gateway-camera-1.tools.kozow.com/capture-image", "Camera setup cho chụp toàn thân.", 1, true, "https://security-gateway-camera-1.tools.kozow.com/libs/index.m3u8" });
        }
    }
}
