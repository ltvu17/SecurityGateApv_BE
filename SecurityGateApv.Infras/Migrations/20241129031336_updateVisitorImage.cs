using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityGateApv.Infras.Migrations
{
    /// <inheritdoc />
    public partial class updateVisitorImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisitorCredentialImage",
                table: "Visitors");

            migrationBuilder.CreateTable(
                name: "VisitorImage",
                columns: table => new
                {
                    VisitorImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitorImage", x => x.VisitorImageId);
                    table.ForeignKey(
                        name: "FK_VisitorImage_Visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitors",
                        principalColumn: "VisitorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitorImage_VisitorId",
                table: "VisitorImage",
                column: "VisitorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitorImage");

            migrationBuilder.AddColumn<string>(
                name: "VisitorCredentialImage",
                table: "Visitors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
