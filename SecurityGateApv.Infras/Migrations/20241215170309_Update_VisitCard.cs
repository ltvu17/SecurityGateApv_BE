using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityGateApv.Infras.Migrations
{
    /// <inheritdoc />
    public partial class Update_VisitCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitCards_VisitDetails_VisitDetailId",
                table: "VisitCards");

            migrationBuilder.RenameColumn(
                name: "VisitDetailId",
                table: "VisitCards",
                newName: "VisitorId");

            migrationBuilder.RenameIndex(
                name: "IX_VisitCards_VisitDetailId",
                table: "VisitCards",
                newName: "IX_VisitCards_VisitorId");

            //migrationBuilder.AddColumn<int>(
            //    name: "CreateById",
            //    table: "Visitors",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Email",
            //    table: "Visitors",
            //    type: "nvarchar(max)",
            //    nullable: true);

            migrationBuilder.InsertData(
                table: "CameraTypes",
                columns: new[] { "CameraTypeId", "CameraTypeName", "Description" },
                values: new object[] { 4, "CheckOut_Body", "Camera chụp toàn thân khi Checkout." });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "Action For Visit Session User", "VisitSession" });

            migrationBuilder.UpdateData(
                table: "ScheduleTypes",
                keyColumn: "ScheduleTypeId",
                keyValue: 1,
                column: "Status",
                value: false);

            migrationBuilder.UpdateData(
                table: "ScheduleTypes",
                keyColumn: "ScheduleTypeId",
                keyValue: 4,
                column: "Status",
                value: false);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Visitors_CreateById",
            //    table: "Visitors",
            //    column: "CreateById");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitCards_Visitors_VisitorId",
                table: "VisitCards",
                column: "VisitorId",
                principalTable: "Visitors",
                principalColumn: "VisitorId",
                onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Visitors_Users_CreateById",
            //    table: "Visitors",
            //    column: "CreateById",
            //    principalTable: "Users",
            //    principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitCards_Visitors_VisitorId",
                table: "VisitCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitors_Users_CreateById",
                table: "Visitors");

            //migrationBuilder.DropIndex(
            //    name: "IX_Visitors_CreateById",
            //    table: "Visitors");

            migrationBuilder.DeleteData(
                table: "CameraTypes",
                keyColumn: "CameraTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "NotificationTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Visitors");

            migrationBuilder.RenameColumn(
                name: "VisitorId",
                table: "VisitCards",
                newName: "VisitDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_VisitCards_VisitorId",
                table: "VisitCards",
                newName: "IX_VisitCards_VisitDetailId");

            migrationBuilder.UpdateData(
                table: "ScheduleTypes",
                keyColumn: "ScheduleTypeId",
                keyValue: 1,
                column: "Status",
                value: true);

            migrationBuilder.UpdateData(
                table: "ScheduleTypes",
                keyColumn: "ScheduleTypeId",
                keyValue: 4,
                column: "Status",
                value: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitCards_VisitDetails_VisitDetailId",
                table: "VisitCards",
                column: "VisitDetailId",
                principalTable: "VisitDetails",
                principalColumn: "VisitDetailId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
