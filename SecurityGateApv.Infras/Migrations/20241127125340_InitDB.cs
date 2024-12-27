using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SecurityGateApv.Infras.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CameraTypes",
                columns: table => new
                {
                    CameraTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CameraTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CameraTypes", x => x.CameraTypeId);
                });

            migrationBuilder.CreateTable(
                name: "CardTypes",
                columns: table => new
                {
                    CardTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTypes", x => x.CardTypeId);
                });

            migrationBuilder.CreateTable(
                name: "CredentialCardTypes",
                columns: table => new
                {
                    CredentialCardTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CredentialCardTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredentialCardTypes", x => x.CredentialCardTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcceptLevel = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Gates",
                columns: table => new
                {
                    GateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gates", x => x.GateId);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrivateKeyServices",
                columns: table => new
                {
                    PrivateKeyServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateKeyServices", x => x.PrivateKeyServiceId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleTypes",
                columns: table => new
                {
                    ScheduleTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTypes", x => x.ScheduleTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardVerification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastCancelDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CardImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_Cards_CardTypes_CardTypeId",
                        column: x => x.CardTypeId,
                        principalTable: "CardTypes",
                        principalColumn: "CardTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    VisitorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CredentialsCard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitorCredentialImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CredentialCardTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.VisitorId);
                    table.ForeignKey(
                        name: "FK_Visitors_CredentialCardTypes_CredentialCardTypeId",
                        column: x => x.CredentialCardTypeId,
                        principalTable: "CredentialCardTypes",
                        principalColumn: "CredentialCardTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cameras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaptureURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreamURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    GateId = table.Column<int>(type: "int", nullable: false),
                    CameraTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cameras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cameras_CameraTypes_CameraTypeId",
                        column: x => x.CameraTypeId,
                        principalTable: "CameraTypes",
                        principalColumn: "CameraTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cameras_Gates_GateId",
                        column: x => x.GateId,
                        principalTable: "Gates",
                        principalColumn: "GateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReadDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    NotificationTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationTypes_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OTP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OTPIssueTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId");
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationUsers",
                columns: table => new
                {
                    NotificationUserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReadStatus = table.Column<bool>(type: "bit", nullable: false),
                    NotificationID = table.Column<int>(type: "int", nullable: false),
                    SenderID = table.Column<int>(type: "int", nullable: false),
                    ReceiverID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationUsers", x => x.NotificationUserID);
                    table.ForeignKey(
                        name: "FK_NotificationUsers_Notifications_NotificationID",
                        column: x => x.NotificationID,
                        principalTable: "Notifications",
                        principalColumn: "NotificationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationUsers_Users_ReceiverID",
                        column: x => x.ReceiverID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationUsers_Users_SenderID",
                        column: x => x.SenderID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DaysOfSchedule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ScheduleTypeId = table.Column<int>(type: "int", nullable: false),
                    CreateById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedules_ScheduleTypes_ScheduleTypeId",
                        column: x => x.ScheduleTypeId,
                        principalTable: "ScheduleTypes",
                        principalColumn: "ScheduleTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Users_CreateById",
                        column: x => x.CreateById,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeadlineTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    AssignToId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleUsers_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleUsers_Users_AssignToId",
                        column: x => x.AssignToId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    VisitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitQuantity = table.Column<int>(type: "int", nullable: false),
                    ExpectedStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpectedEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateById = table.Column<int>(type: "int", nullable: false),
                    UpdateById = table.Column<int>(type: "int", nullable: true),
                    ScheduleUserId = table.Column<int>(type: "int", nullable: true),
                    ResponsiblePersonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.VisitId);
                    table.ForeignKey(
                        name: "FK_Visits_ScheduleUsers_ScheduleUserId",
                        column: x => x.ScheduleUserId,
                        principalTable: "ScheduleUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Visits_Users_CreateById",
                        column: x => x.CreateById,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visits_Users_ResponsiblePersonId",
                        column: x => x.ResponsiblePersonId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Visits_Users_UpdateById",
                        column: x => x.UpdateById,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VisitDetails",
                columns: table => new
                {
                    VisitDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpectedStartHour = table.Column<TimeSpan>(type: "time", nullable: false),
                    ExpectedEndHour = table.Column<TimeSpan>(type: "time", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    VisitId = table.Column<int>(type: "int", nullable: false),
                    VisitorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitDetails", x => x.VisitDetailId);
                    table.ForeignKey(
                        name: "FK_VisitDetails_Visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitors",
                        principalColumn: "VisitorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitDetails_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleSessions",
                columns: table => new
                {
                    VehicleSessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckinTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckoutTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitDetailId = table.Column<int>(type: "int", nullable: false),
                    SecurityInId = table.Column<int>(type: "int", nullable: false),
                    SecurityOutId = table.Column<int>(type: "int", nullable: true),
                    GateInId = table.Column<int>(type: "int", nullable: false),
                    GateOutId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleSessions", x => x.VehicleSessionId);
                    table.ForeignKey(
                        name: "FK_VehicleSessions_Gates_GateInId",
                        column: x => x.GateInId,
                        principalTable: "Gates",
                        principalColumn: "GateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleSessions_Gates_GateOutId",
                        column: x => x.GateOutId,
                        principalTable: "Gates",
                        principalColumn: "GateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleSessions_Users_SecurityInId",
                        column: x => x.SecurityInId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleSessions_Users_SecurityOutId",
                        column: x => x.SecurityOutId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleSessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_VehicleSessions_VisitDetails_VisitDetailId",
                        column: x => x.VisitDetailId,
                        principalTable: "VisitDetails",
                        principalColumn: "VisitDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitCards",
                columns: table => new
                {
                    VisitCardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitCardStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitDetailId = table.Column<int>(type: "int", nullable: false),
                    CardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitCards", x => x.VisitCardId);
                    table.ForeignKey(
                        name: "FK_VisitCards_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitCards_VisitDetails_VisitDetailId",
                        column: x => x.VisitDetailId,
                        principalTable: "VisitDetails",
                        principalColumn: "VisitDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitorSessions",
                columns: table => new
                {
                    VisitorSessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckinTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckoutTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitDetailId = table.Column<int>(type: "int", nullable: false),
                    SecurityInId = table.Column<int>(type: "int", nullable: false),
                    SecurityOutId = table.Column<int>(type: "int", nullable: true),
                    GateInId = table.Column<int>(type: "int", nullable: false),
                    GateOutId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitorSessions", x => x.VisitorSessionId);
                    table.ForeignKey(
                        name: "FK_VisitorSessions_Gates_GateInId",
                        column: x => x.GateInId,
                        principalTable: "Gates",
                        principalColumn: "GateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitorSessions_Gates_GateOutId",
                        column: x => x.GateOutId,
                        principalTable: "Gates",
                        principalColumn: "GateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitorSessions_Users_SecurityInId",
                        column: x => x.SecurityInId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitorSessions_Users_SecurityOutId",
                        column: x => x.SecurityOutId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitorSessions_VisitDetails_VisitDetailId",
                        column: x => x.VisitDetailId,
                        principalTable: "VisitDetails",
                        principalColumn: "VisitDetailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleSessionImages",
                columns: table => new
                {
                    VehicleSessionImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleSessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleSessionImages", x => x.VehicleSessionImageId);
                    table.ForeignKey(
                        name: "FK_VehicleSessionImages_VehicleSessions_VehicleSessionId",
                        column: x => x.VehicleSessionId,
                        principalTable: "VehicleSessions",
                        principalColumn: "VehicleSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitorSessionsImages",
                columns: table => new
                {
                    VisitorSessionsImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitorSessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitorSessionsImages", x => x.VisitorSessionsImageId);
                    table.ForeignKey(
                        name: "FK_VisitorSessionsImages_VisitorSessions_VisitorSessionId",
                        column: x => x.VisitorSessionId,
                        principalTable: "VisitorSessions",
                        principalColumn: "VisitorSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CameraTypes",
                columns: new[] { "CameraTypeId", "CameraTypeName", "Description" },
                values: new object[,]
                {
                    { 1, "Visitor_Body", "Camera chụp toàn thân." },
                    { 2, "Visitor_Shoe", "Camera chụp giày." }
                });

            migrationBuilder.InsertData(
                table: "CardTypes",
                columns: new[] { "CardTypeId", "CardTypeName", "TypeDescription" },
                values: new object[,]
                {
                    { 1, "ShotTermCard", "Thẻ cho ra vào hằng ngày" },
                    { 2, "LongTermCard", "Thẻ cho ra vào theo lịch trình" }
                });

            migrationBuilder.InsertData(
                table: "CredentialCardTypes",
                columns: new[] { "CredentialCardTypeId", "CredentialCardTypeName", "Description", "Status" },
                values: new object[,]
                {
                    { 1, "Căn cước công dân", "Căn cước công dân", true },
                    { 2, "Giấy phép lái xe", "Giấy phép lái xe", true }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "AcceptLevel", "CreateDate", "DepartmentName", "Description", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Phòng ban riêng cho admin", "Active", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manager", "Phòng ban riêng cho quản lý", "Active", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Security", "Phòng ban riêng cho quản security", "Active", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phòng Nhân sự", "Phòng nhân sự", "Active", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phòng Sản xuất", "Phòng Sản xuất", "Active", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Gates",
                columns: new[] { "GateId", "CreateDate", "Description", "GateName", "Status" },
                values: new object[] { 1, new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cổng A", "Cổng A", true });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Action For Visit", "Visit" },
                    { 2, "Action For Schedule User", "ScheduleUser" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Description", "RoleName" },
                values: new object[,]
                {
                    { 1, "Quản lý toàn bộ hệ thống", "Admin" },
                    { 2, "Quản lý toàn bộ công ty", "Manager" },
                    { 3, "Quản lý toàn bộ phòng ban", "DepartmentManager" },
                    { 4, "Tạo và quản lý khách ra vào của phòng ban", "Staff" },
                    { 5, "Quản lý khách ra vào tại cổng", "Security" }
                });

            migrationBuilder.InsertData(
                table: "ScheduleTypes",
                columns: new[] { "ScheduleTypeId", "Description", "ScheduleTypeName", "Status" },
                values: new object[,]
                {
                    { 1, "Lịch trình đăng ký ra vào hàng ngày cho staff và bảo vệ", "VisitDaily", true },
                    { 2, "Lịch trình đăng ký ra vào theo tiến trình hàng tuần cho Department Manager", "ProcessWeek", true },
                    { 3, "Lịch trình đăng ký ra vào theo tiến trình hàng tháng cho Department Manager", "ProcessMonth", true },
                    { 4, "Lịch trình đăng ký ra vào theo dự án cho Department Manager", "Project", true }
                });

            migrationBuilder.InsertData(
                table: "Cameras",
                columns: new[] { "Id", "CameraTypeId", "CaptureURL", "Description", "GateId", "Status", "StreamURL" },
                values: new object[] { 1, 1, "https://security-gateway-camera-1.tools.kozow.com/capture-image", "Camera setup cho chụp toàn thân.", 1, true, "https://security-gateway-camera-1.tools.kozow.com/libs/index.m3u8" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedDate", "DepartmentId", "Email", "FullName", "Image", "OTP", "OTPIssueTime", "Password", "PhoneNumber", "RoleId", "Status", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "admin1@example.com", "Admin One", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdCMjLlNPwkWsEFRDeMI8rLlWCVs4mbaa-Xg&s", null, null, "123", "0123456789", 1, "Active", new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin1" },
                    { 2, new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "manager1@example.com", "Manager One", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdCMjLlNPwkWsEFRDeMI8rLlWCVs4mbaa-Xg&s", null, null, "123", "0987654321", 2, "Active", new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "manager1" },
                    { 3, new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "employee1@example.com", "Department Manager One", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdCMjLlNPwkWsEFRDeMI8rLlWCVs4mbaa-Xg&s", null, null, "123", "0112223334", 3, "Active", new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "DM1" },
                    { 4, new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Staff1@egmail.com", "Staff One", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRE4g-3ZH_1TjfN-zOuCRru2LrfrGtPbwaCsQ&s", null, null, "123", "0223334445", 4, "Active", new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Staff1" },
                    { 5, new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Staff2@gmail.com", "Staff Tow", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdCMjLlNPwkWsEFRDeMI8rLlWCVs4mbaa-Xg&s", null, null, "123", "0223334446", 4, "Active", new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Staff2" },
                    { 6, new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "cuong3right@gmail.com", "Quốc Cường", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQu0qRJBJeHYTEukW7kTEAW8UMznPMxnuIziw&s", null, null, "123", "0355004120", 5, "Active", new DateTime(2024, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Security1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cameras_CameraTypeId",
                table: "Cameras",
                column: "CameraTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cameras_GateId",
                table: "Cameras",
                column: "GateId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardTypeId",
                table: "Cards",
                column: "CardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationTypeId",
                table: "Notifications",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationUsers_NotificationID",
                table: "NotificationUsers",
                column: "NotificationID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationUsers_ReceiverID",
                table: "NotificationUsers",
                column: "ReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationUsers_SenderID",
                table: "NotificationUsers",
                column: "SenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_CreateById",
                table: "Schedules",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ScheduleTypeId",
                table: "Schedules",
                column: "ScheduleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleUsers_AssignToId",
                table: "ScheduleUsers",
                column: "AssignToId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleUsers_ScheduleId",
                table: "ScheduleUsers",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSessionImages_VehicleSessionId",
                table: "VehicleSessionImages",
                column: "VehicleSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSessions_GateInId",
                table: "VehicleSessions",
                column: "GateInId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSessions_GateOutId",
                table: "VehicleSessions",
                column: "GateOutId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSessions_SecurityInId",
                table: "VehicleSessions",
                column: "SecurityInId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSessions_SecurityOutId",
                table: "VehicleSessions",
                column: "SecurityOutId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSessions_UserId",
                table: "VehicleSessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleSessions_VisitDetailId",
                table: "VehicleSessions",
                column: "VisitDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitCards_CardId",
                table: "VisitCards",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitCards_VisitDetailId",
                table: "VisitCards",
                column: "VisitDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitDetails_VisitId",
                table: "VisitDetails",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitDetails_VisitorId",
                table: "VisitDetails",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitors_CredentialCardTypeId",
                table: "Visitors",
                column: "CredentialCardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorSessions_GateInId",
                table: "VisitorSessions",
                column: "GateInId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorSessions_GateOutId",
                table: "VisitorSessions",
                column: "GateOutId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorSessions_SecurityInId",
                table: "VisitorSessions",
                column: "SecurityInId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorSessions_SecurityOutId",
                table: "VisitorSessions",
                column: "SecurityOutId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorSessions_VisitDetailId",
                table: "VisitorSessions",
                column: "VisitDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitorSessionsImages_VisitorSessionId",
                table: "VisitorSessionsImages",
                column: "VisitorSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_CreateById",
                table: "Visits",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_ResponsiblePersonId",
                table: "Visits",
                column: "ResponsiblePersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_ScheduleUserId",
                table: "Visits",
                column: "ScheduleUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_UpdateById",
                table: "Visits",
                column: "UpdateById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cameras");

            migrationBuilder.DropTable(
                name: "NotificationUsers");

            migrationBuilder.DropTable(
                name: "PrivateKeyServices");

            migrationBuilder.DropTable(
                name: "VehicleSessionImages");

            migrationBuilder.DropTable(
                name: "VisitCards");

            migrationBuilder.DropTable(
                name: "VisitorSessionsImages");

            migrationBuilder.DropTable(
                name: "CameraTypes");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "VehicleSessions");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "VisitorSessions");

            migrationBuilder.DropTable(
                name: "NotificationTypes");

            migrationBuilder.DropTable(
                name: "CardTypes");

            migrationBuilder.DropTable(
                name: "Gates");

            migrationBuilder.DropTable(
                name: "VisitDetails");

            migrationBuilder.DropTable(
                name: "Visitors");

            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "CredentialCardTypes");

            migrationBuilder.DropTable(
                name: "ScheduleUsers");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "ScheduleTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
