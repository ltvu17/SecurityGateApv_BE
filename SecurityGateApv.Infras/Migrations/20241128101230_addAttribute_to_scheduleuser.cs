using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityGateApv.Infras.Migrations
{
    /// <inheritdoc />
    public partial class addAttribute_to_scheduleuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "ScheduleUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxPersonQuantity",
                table: "ScheduleUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "ScheduleUsers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "ScheduleUsers");

            migrationBuilder.DropColumn(
                name: "MaxPersonQuantity",
                table: "ScheduleUsers");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "ScheduleUsers");
        }
    }
}
