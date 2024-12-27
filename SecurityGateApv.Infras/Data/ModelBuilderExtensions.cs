using Bogus;
using Microsoft.EntityFrameworkCore;
using SecurityGateApv.Domain.Enums;
using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Infras.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            SeedRoles(modelBuilder);
            SeedFixedUsers(modelBuilder);
            //SeedRandomUsers(modelBuilder);
            SeedDepartments(modelBuilder);
            //SeedUserDepartments(modelBuilder);
            SeedCredentialCardTypes(modelBuilder);
            SeedScheduleTypes(modelBuilder);
            //SeedVisitors(modelBuilder);
            //SeedVisitType(modelBuilder);
            //SeedProcess(modelBuilder);
            //SeedVisits(modelBuilder);
            //SeedVisitDetails(modelBuilder);
            SeedQRCards(modelBuilder);
            SeedGate(modelBuilder);
            SeedNotiTypes(modelBuilder);
            //SeedVisitorSession(modelBuilder);

        }

        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Admin", Description = "Quản lý toàn bộ hệ thống" },
                new Role { RoleId = 2, RoleName = "Manager", Description = "Quản lý toàn bộ công ty" },
                new Role { RoleId = 3, RoleName = "DepartmentManager", Description = "Quản lý toàn bộ phòng ban" },
                new Role { RoleId = 4, RoleName = "Staff", Description = "Tạo và quản lý khách ra vào của phòng ban" },
                new Role { RoleId = 5, RoleName = "Security", Description = "Quản lý khách ra vào tại cổng" }
            );
        }
        private static void SeedNotiTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotificationType>().HasData(
                new NotificationType { Id = 1, Name = "Visit", Description = "Action For Visit" },
                new NotificationType { Id = 2, Name = "ScheduleUser", Description = "Action For Schedule User" },
                new NotificationType { Id = 3, Name = "VisitSession", Description = "Action For Visit Session User" }
            );
        }

        private static void SeedFixedUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                 User.Create(1, "admin1", "123", "Admin One", "admin1@example.com", "0123456789",
                 "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdCMjLlNPwkWsEFRDeMI8rLlWCVs4mbaa-Xg&s",
                     new DateTime(2024, 09, 29),
                     new DateTime(2024, 09, 29),
                    "Active",
                   1,
                   1
                ).Value,


                User.Create(

                     2,
                     "manager1",
                    "123",
                    "Manager One",
                    "manager1@example.com",
                    "0987654321",
                    "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdCMjLlNPwkWsEFRDeMI8rLlWCVs4mbaa-Xg&s",
                     new DateTime(2024, 09, 29),
                     new DateTime(2024, 09, 29),
                    "Active",
                    2, 2
                ).Value,
                User.Create(
                    3,
                    "DM1",
                    "123",
                    "Department Manager One",
                    "employee1@example.com",
                    "0112223334",
                    "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdCMjLlNPwkWsEFRDeMI8rLlWCVs4mbaa-Xg&s",
                    new DateTime(2024, 09, 29),
                 new DateTime(2024, 09, 29),
                    "Active",
                     3,
                     4
                ).Value,
                User.Create(
                     4,
                    "Staff1",
                    "123",
                    "Staff One",
                    "Staff1@egmail.com",
                    "0223334445",
                    "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRE4g-3ZH_1TjfN-zOuCRru2LrfrGtPbwaCsQ&s",
                    new DateTime(2024, 09, 29),
                 new DateTime(2024, 09, 29),
                    "Active",
                     4,
                     4
                ).Value,
                User.Create(
                     5,
                    "Staff2",
                    "123",
                    "Staff Tow",
                    "Staff2@gmail.com",
                    "0223334446",
                    "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdCMjLlNPwkWsEFRDeMI8rLlWCVs4mbaa-Xg&s",
                    new DateTime(2024, 09, 29),
                 new DateTime(2024, 09, 29),
                    "Active",
                     4,
                     4
                ).Value,
               User.Create(
                    6,
                    "Security1",
                    "123",
                    "Quốc Cường",
                    "cuong3right@gmail.com",
                    "0355004120",
                    "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQu0qRJBJeHYTEukW7kTEAW8UMznPMxnuIziw&s",
                    new DateTime(2024, 09, 29),
                 new DateTime(2024, 09, 29),
                    "Active",
                     5,
                     3
                ).Value
            );
        }

        private static void SeedRandomUsers(ModelBuilder modelBuilder)
        {
            var userFaker = new Faker<User>()
                .RuleFor(u => u.UserId, f => f.IndexFaker + 6)
                .RuleFor(u => u.UserName, f => f.Internet.UserName())
                .RuleFor(u => u.Password, f => f.Random.AlphaNumeric(6))
                .RuleFor(u => u.FullName, f => f.Name.FullName())
                .RuleFor(u => u.Email, f => f.Internet.Email("gmail.com"))
                .RuleFor(u => u.PhoneNumber, f => f.Random.Number(100000000, 999999999).ToString("D10"))
                .RuleFor(u => u.CreatedDate, f => f.Date.Between(new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1), DateTime.UtcNow))
                .RuleFor(u => u.UpdatedDate, (f, u) => u.CreatedDate)
                .RuleFor(u => u.Status, f => "Active")
                .RuleFor(u => u.RoleId, f => f.Random.Int(1, 5));

            var randomUsers = userFaker.Generate(20);

            modelBuilder.Entity<User>().HasData(randomUsers);
        }
        private static void SeedDepartments(ModelBuilder modelBuilder)
        {
            //var departmentNames = new List<string>
            //{
            //    "Phòng Nhân sự",
            //    "Phòng Kế toán",
            //    "Phòng IT",
            //    "Phòng Kinh doanh",
            //    "Phòng Marketing",
            //    "Phòng Hành chính",
            //    "Phòng Chăm sóc khách hàng",
            //    "Phòng Pháp chế",
            //    "Phòng R&D",
            //    "Phòng Sản xuất"
            //};

            //var departmentFaker = new Faker<Department>()
            //    .RuleFor(d => d.DepartmentId, f => f.IndexFaker + 1)
            //    .RuleFor(d => d.DepartmentName, f => f.PickRandom(departmentNames))
            //    .RuleFor(d => d.Description, f => f.Lorem.Sentence())
            //    .RuleFor(d => d.CreateDate, f => new DateTime(2024, 09, 29))
            //    .RuleFor(d => d.UpdatedDate, (f, d) => d.CreateDate)
            //    .RuleFor(d => d.AcceptLevel, f => 2);

            //var randomDepartments = departmentFaker.Generate(10);

            var departmentList = new List<Department>();
            departmentList.Add(Department.Create(1, "Admin", new DateTime(2024, 09, 29), new DateTime(2024, 09, 29), "Phòng ban riêng cho admin", 1, "Active").Value);
            departmentList.Add(Department.Create(2, "Manager", new DateTime(2024, 09, 29), new DateTime(2024, 09, 29), "Phòng ban riêng cho quản lý", 1, "Active").Value);
            departmentList.Add(Department.Create(3, "Security", new DateTime(2024, 09, 29), new DateTime(2024, 09, 29), "Phòng ban riêng cho quản security", 1, "Active").Value);
            departmentList.Add(Department.Create(4, "Phòng Nhân sự", new DateTime(2024, 09, 29), new DateTime(2024, 09, 29), "Phòng nhân sự", 1, "Active").Value);
            departmentList.Add(Department.Create(5, "Phòng Sản xuất", new DateTime(2024, 09, 29), new DateTime(2024, 09, 29), "Phòng Sản xuất", 1, "Active").Value);


            modelBuilder.Entity<Department>().HasData(departmentList);
        }
        /*private static void SeedUserDepartments(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserDepartment>().HasData(
                UserDepartment.Create(1, 1, 4).Value,
                UserDepartment.Create(2, 1, 3).Value,
                UserDepartment.Create(3, 2, 3).Value,
                UserDepartment.Create(4, 2, 5).Value
                );
        }*/


        private static void SeedCredentialCardTypes(ModelBuilder modelBuilder)
        {
            var cardTypes = new List<CredentialCardType>();

            var cardType1Result = CredentialCardType.Create(1, "Căn cước công dân", "Căn cước công dân", true);
            var cardType3Result = CredentialCardType.Create(2, "Giấy phép lái xe", "Giấy phép lái xe", true);

            if (cardType1Result.IsSuccess) cardTypes.Add(cardType1Result.Value);
            if (cardType3Result.IsSuccess) cardTypes.Add(cardType3Result.Value);

            modelBuilder.Entity<CredentialCardType>().HasData(cardTypes);
        }
        private static void SeedScheduleTypes(ModelBuilder modelBuilder)
        {
            var scheduleTypeList = new List<ScheduleType>();
            scheduleTypeList.Add(ScheduleType.Create(1, ScheduleTypeEnum.VisitDaily.ToString(), "Lịch trình đăng ký ra vào hàng ngày cho staff và bảo vệ", false).Value);
            scheduleTypeList.Add(ScheduleType.Create(2, ScheduleTypeEnum.ProcessWeek.ToString(), "Lịch trình đăng ký ra vào theo tiến trình hàng tuần cho Department Manager", true).Value);
            scheduleTypeList.Add(ScheduleType.Create(3, ScheduleTypeEnum.ProcessMonth.ToString(), "Lịch trình đăng ký ra vào theo tiến trình hàng tháng cho Department Manager", true).Value);
            scheduleTypeList.Add(ScheduleType.Create(4, ScheduleTypeEnum.Project.ToString(), "Lịch trình đăng ký ra vào theo dự án cho Department Manager", false).Value);

            modelBuilder.Entity<ScheduleType>().HasData(scheduleTypeList);

            //var scheduleDaily = Schedule.Create(1, "Lịch đăng ký hằng ngày", "", 0, "Lịch đăng kí hàng ngày không được sửa hoặc thêm", new DateTime(2024, 09, 29), new DateTime(2024, 10, 24), true, 1, 1);
            //modelBuilder.Entity<Schedule>().HasData(scheduleDaily);
        }

        private static void SeedVisitors(ModelBuilder modelBuilder)
        {
            var visitorFaker = new Faker<Visitor>()
                .RuleFor(v => v.VisitorId, f => f.IndexFaker + 1)
                .RuleFor(v => v.VisitorName, f => f.Name.FullName())
                //.RuleFor(v => v.CompanyName, f => f.Company.CompanyName())
                .RuleFor(v => v.PhoneNumber, f => f.Random.Number(100000000, 999999999).ToString("D10"))
                //.RuleFor(v => v.CreatedDate, f => f.Date.Between(new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1), DateTime.UtcNow))
                //.RuleFor(v => v.UpdatedDate, (f, v) => v.CreatedDate)
                .RuleFor(v => v.CredentialsCard, f => f.Random.AlphaNumeric(10))
                //.RuleFor(v => v.Status, f => f.Random.Bool())
                //.RuleFor(v => v.UserId, f => null)
                .RuleFor(v => v.CredentialCardTypeId, f => f.PickRandom(1, 2));
            //.RuleFor(v => v.CreateById, f => 1);

            var visitors = visitorFaker.Generate(10);

            modelBuilder.Entity<Visitor>().HasData(visitors);
        }
        /*private static void SeedVisitType(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScheduleType>().HasData(
                new ScheduleType { VisitTypeId = 1, VisitTypeName = "ProcessWeek", Description = "Process trong tuần" },
                new ScheduleType { VisitTypeId = 2, VisitTypeName = "ProcessMonth", Description = "Process trong tháng" },
                new ScheduleType { VisitTypeId = 3, VisitTypeName = "Project", Description = "Project" },
                new ScheduleType { VisitTypeId = 4, VisitTypeName = "VisitStaff", Description = "Visit được tạo bởi staff" },
                new ScheduleType { VisitTypeId = 5, VisitTypeName = "VisitSecurity", Description = "Visit được tạo bởi security" }
            );
        }*/
        /*private static void SeedProcess(ModelBuilder modelBuilder)
        {
            var ProcessFaker = new Faker<Schedule>()
             .RuleFor(p => p.ProcessId, f => f.IndexFaker + 1)
             .RuleFor(p => p.ProcessName, f => f.Commerce.ProductName())
             .RuleFor(p => p.CreateTime, f => DateTime.UtcNow.AddDays(-2))
             .RuleFor(p => p.Description, f => "")
             .RuleFor(p => p.Status, f => f.Random.Bool())
             .RuleFor(p => p.CreateBy, f => 3)
             .RuleFor(p => p.VisitTypeId, f => 1);

            var processes = ProcessFaker.Generate(5);

            modelBuilder.Entity<Schedule>().HasData(processes);

            var visitFaker = new Faker<Visit>()
                .RuleFor(v => v.VisitId, f => f.IndexFaker + 1)
                .RuleFor(v => v.DateRegister, f => DateTime.Now.AddDays(-2))
                .RuleFor(v => v.VisitName, f => "Dọn vệ sinh")
                .RuleFor(v => v.VisitQuantity, f => 2)
                .RuleFor(v => v.AcceptLevel, f => 1)
                .RuleFor(v => v.Description, f => null)
                .RuleFor(v => v.VisitType, f => "ProcessWeek")
                .RuleFor(v => v.CreateById, f => 3)
                .RuleFor(v => v.UpdateById, f => 4);

            var visits = visitFaker.Generate(1);
            modelBuilder.Entity<Visit>().HasData(visits);


            var visitProcessFaker = new Faker<VisitProcess>()
                .RuleFor(v => v.VisitProcessId, f => f.IndexFaker + 1)
                .RuleFor(v => v.ExpectedStartDate, f => DateTime.Now.AddDays(-1))
                .RuleFor(v => v.ExpectedEndDate, f => DateTime.Now.AddDays(29))
                .RuleFor(v => v.ExpectedStartTime, f => TimeSpan.FromHours(7))
                .RuleFor(v => v.ExpectedEndTime, f => TimeSpan.FromHours(12))
                .RuleFor(v => v.DaysOfProcess, f => "Monday")
                .RuleFor(v => v.VisitQuantity, f => 2)
                .RuleFor(v => v.Status, f => "Processing")
                .RuleFor(v => v.ProcessId, f => 1)
                .RuleFor(v => v.VisitId, f => 1);

            var visitProcess = visitProcessFaker.Generate(1);
            modelBuilder.Entity<VisitProcess>().HasData(visitProcess);


            var visitDetailFaker = new Faker<VisitDetail>()
               .RuleFor(vd => vd.VisitDetailId, f => f.IndexFaker + 1)
               .RuleFor(vd => vd.ExpectedStartDate, f => DateTime.Now.AddDays(-1))
               .RuleFor(vd => vd.ExpectedEndDate, f => DateTime.Now.AddDays(29))
               .RuleFor(vd => vd.ExpectedStartTime, f => TimeSpan.FromHours(7))
               .RuleFor(vd => vd.ExpectedEndTime, f => TimeSpan.FromHours(12))
               .RuleFor(vd => vd.Description, f => f.Lorem.Paragraph())
               .RuleFor(vd => vd.Status, f => true)
               .RuleFor(vd => vd.VisitId, f => 1)
               .RuleFor(vd => vd.VisitorId, f => f.Random.Int(1, 2));

            var visitDetails = visitDetailFaker.Generate(2);

            modelBuilder.Entity<VisitDetail>().HasData(visitDetails);

        }*/
        /* private static void SeedVisits(ModelBuilder modelBuilder)
         {
             var visitFaker = new Faker<Visit>()
                 .RuleFor(v => v.VisitId, f => f.IndexFaker + 2)
                 .RuleFor(v => v.DateRegister, f => DateTime.UtcNow)
                 .RuleFor(v => v.VisitName, f => "Dọn vệ sinh")
                 .RuleFor(v => v.VisitQuantity, f => 2)
                 .RuleFor(v => v.AcceptLevel, f => 2)
                 .RuleFor(v => v.Description, f => null)
                 .RuleFor(v => v.VisitType, f => "ProcessWeek")
                 .RuleFor(v => v.CreateById, f => 4)
                 .RuleFor(v => v.UpdateById, f => 4);
             var visits = visitFaker.Generate(5);
             modelBuilder.Entity<Visit>().HasData(visits);



             int visitDetailCounter = 3;
             foreach (var v in visits)
             {
                 for (var v2 = 0; v2 <= v.VisitQuantity; v2++)
                 {

                     var visitDetailFaker = new Faker<VisitDetail>()
                       .RuleFor(vd => vd.VisitDetailId, f => visitDetailCounter++)
                       .RuleFor(vd => vd.Description, f => f.Lorem.Paragraph())
                       .RuleFor(vd => vd.ExpectedStartDate, f => DateTime.Now)
                       .RuleFor(vd => vd.ExpectedEndDate, f => DateTime.Now.AddDays(1))
                       .RuleFor(vd => vd.ExpectedStartTime, f => TimeSpan.FromHours(7))
                       .RuleFor(vd => vd.ExpectedEndTime, f => TimeSpan.FromHours(12))
                       .RuleFor(vd => vd.Status, f => true)
                       .RuleFor(vd => vd.VisitId, f => v.VisitId)
                       .RuleFor(vd => vd.VisitorId, f => f.Random.Int(1, 10));

                     var visitDetails = visitDetailFaker.Generate(v.VisitQuantity);

                     modelBuilder.Entity<VisitDetail>().HasData(visitDetails);
                 }
             }

         }*/

        private static void SeedVisitDetails(ModelBuilder modelBuilder)
        {

        }

        private static void SeedQRCards(ModelBuilder modelBuilder)
        {
            // Seed QRCardStatus
            //modelBuilder.Entity<QRCardStatus>().HasData(
            //    new QRCardStatus { QRCardStatusId = 1, StatusName = "Active", Description = "Thẻ đang được dùng checkin" },
            //    new QRCardStatus { QRCardStatusId = 2, StatusName = "Inactive", Description = "Thẻ chưa dùng cho bảo vệ" },
            //    new QRCardStatus { QRCardStatusId = 3, StatusName = "Disable", Description = "Thẻ đã hết hạn" }
            //);

            // Seed QRCardType
            modelBuilder.Entity<CardType>().HasData(
                new CardType { CardTypeId = 1, CardTypeName = CardTypeEnum.ShotTermCard.ToString(), TypeDescription = "Thẻ cho ra vào hằng ngày" },
                new CardType { CardTypeId = 2, CardTypeName = CardTypeEnum.LongTermCard.ToString(), TypeDescription = "Thẻ cho ra vào theo lịch trình" }
            );

            /*var cardFaker = new Faker<QRCard>()
                .RuleFor(q => q.QRCardId, f => f.IndexFaker + 1)
                .RuleFor(q => q.CardVerification, f => f.Random.AlphaNumeric(10))
                .RuleFor(q => q.CreateDate, f => DateTime.Now)  // CreateDate là hôm nay
                .RuleFor(q => q.LastCancelDate, f => DateTime.Now)  // LastCancelDate là 1 tháng sau CreateDate
                .RuleFor(q => q.QRCardTypeId, f => f.PickRandom(1, 2))  // Random QRCardType
                .RuleFor(q => q.QRCardStatusId, f => f.PickRandom(1, 2)); */ // Random QRCardStatus


            //var randomQRCards = cardFaker.Generate(10);

            // Seed QRCards
            //modelBuilder.Entity<QRCard>().HasData(randomQRCards);
        }
        private static void SeedGate(ModelBuilder modelBuilder)
        {
            var cameraTypeList = new List<CameraType>
                {
                    CameraType.Create(1, ImageTypeEnum.CheckIn_Shoe.ToString(), "Camera chụp giày khi checkin.").Value,
                    CameraType.Create(2, ImageTypeEnum.CheckIn_Body.ToString(), "Camera chụp toàn thân khi checkin.").Value,
                    CameraType.Create(3, ImageTypeEnum.CheckOut_Shoe.ToString(), "Camera chụp giày khi Checkout.").Value,
                    CameraType.Create(4, ImageTypeEnum.CheckOut_Body.ToString(), "Camera chụp toàn thân khi Checkout.").Value
                };

            //var gateList = new List<Gate>
            //    {
            //        Gate.Create(1, "Cổng A", new DateTime(2024, 09, 29), "Cổng A", true).Value
            //    };

            //var cameraList = new List<Camera>
            //    {
            //        Camera.Create(1, "https://security-gateway-camera-1.tools.kozow.com/capture-image",
            //            "https://security-gateway-camera-1.tools.kozow.com/libs/index.m3u8",
            //            "Camera setup cho chụp toàn thân.",
            //            true, 1, 1).Value
            //    };

            modelBuilder.Entity<CameraType>().HasData(cameraTypeList);
            //modelBuilder.Entity<Gate>().HasData(gateList);
            //modelBuilder.Entity<Camera>().HasData(cameraList);
        }
        private static void SeedVisitorSession(ModelBuilder modelBuilder)
        {
            var visitorSessionFaker = new Faker<VisitorSession>()
                .RuleFor(vs => vs.VisitorSessionId, f => f.IndexFaker + 1)
                .RuleFor(vs => vs.CheckinTime, f => DateTime.UtcNow.AddDays(1))
                .RuleFor(vs => vs.CheckoutTime, f => null)
                //.RuleFor(vs => vs.CardId, f => f.Random.Int(1, 10))
                .RuleFor(vs => vs.VisitDetailId, f => f.Random.Int(1, 10))
                .RuleFor(vs => vs.SecurityInId, 5)
                .RuleFor(vs => vs.SecurityOutId, f => null)
                .RuleFor(vs => vs.GateInId, f => f.Random.Int(1, 2))
                .RuleFor(vs => vs.GateOutId, f => null)
                .RuleFor(vs => vs.Status, f => "CheckIn");

            var visitorSessions = visitorSessionFaker.Generate(5);
            modelBuilder.Entity<VisitorSession>().HasData(visitorSessions);
            //SeedVisitorSessionImages(modelBuilder, visitorSessions);
        }

        /*private static void SeedVisitorSessionImages(ModelBuilder modelBuilder, List<VisitorSession> visitorSessions)
        {
            var visitorSessionImages = new List<VisitorSessionsImage>();

            foreach (var session in visitorSessions)
            {
                for (int j = 0; j < 3; j++) // Tạo 3 hình ảnh cho mỗi VisitorSession
                {
                    visitorSessionImages.Add(new VisitorSessionsImage
                    {
                        VisitorSessionsImageId = (session.VisitorSessionId - 1) * 3 + j + 1, // Đảm bảo ID là duy nhất
                        ImageType = "jpg", // Hoặc bất kỳ loại nào bạn muốn
                        ImageURL = $"https://example.com/images/{session.VisitorSessionId}_{j + 1}.jpg", // Địa chỉ URL hình ảnh
                        VisitorSessionId = session.VisitorSessionId
                    });
                }
            }

            modelBuilder.Entity<VisitorSessionsImage>().HasData(visitorSessionImages);
        }*/



    }

}
