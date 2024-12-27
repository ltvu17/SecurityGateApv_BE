using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityGateApv.Infras.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVisitor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreateById",
                table: "Visitors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Visitors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_CreateById",
                table: "Visitors",
                column: "CreateById");

            migrationBuilder.AddForeignKey(
                name: "FK_Visitors_Users_CreateById",
                table: "Visitors",
                column: "CreateById",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visitors_Users_CreateById",
                table: "Visitors");

            migrationBuilder.DropIndex(
                name: "IX_Visitors_CreateById",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Visitors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Visitors");
        }
    }
}
