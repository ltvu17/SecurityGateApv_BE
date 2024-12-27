using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SecurityGateApv.Domain.Interfaces.AWS;
using SecurityGateApv.Domain.Interfaces.DomainDTOs;
using SecurityGateApv.Domain.Interfaces.EmailSender;
using SecurityGateApv.Domain.Interfaces.ExtractImage;
using SecurityGateApv.Domain.Interfaces.Jwt;
using SecurityGateApv.Domain.Interfaces.Notifications;
using SecurityGateApv.Domain.Interfaces.Repositories;
using SecurityGateApv.Infras.AWS;
using SecurityGateApv.Infras.BackgroundWorker;
using SecurityGateApv.Infras.DBContext;
using SecurityGateApv.Infras.Helpers;
using SecurityGateApv.Infras.Notifications;
using SecurityGateApv.Infras.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Infras.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString, IConfiguration configuration)
        {
            services.AddDbContext<SecurityGateApvDbContext>(
                options =>
                {
                    options.UseSqlServer(connectionString);
                });

            //DI Core
            services.AddScoped<IUnitOfWork, UnitOfWork<SecurityGateApvDbContext>>();

            //DI Repo
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IVisitRepo, VisitRepo>();
            services.AddScoped<IVisitDetailRepo, VisitDetailRepo>();
            services.AddScoped<IVisitorSessionRepo, VisitorSessionRepo>();
            services.AddScoped<IVehicleSessionRepo, VehicleSessionRepo>();
            services.AddScoped<IGateRepo, GateRepo>();
            services.AddScoped<ICameraRepo, CameraRepo>();
            services.AddScoped<ICameraTypeRepo, CameraTypeRepo>();
            services.AddScoped<IGateRepo, GateRepo>();
            services.AddScoped<ICardRepo, CardRepo>();
            services.AddScoped<ICardTypeRepo, CardTypeRepo>();
            services.AddScoped<IVisitorRepo, VisitorRepo>();
            services.AddScoped<IScheduleTypeRepo, ScheduleTypeRepo>();
            services.AddScoped<IScheduleRepo, ScheduleRepo>();
            services.AddScoped<IDepartmentRepo, DepartmentRepo>();
            services.AddScoped<IRoleRepo, RoleRepo>();
            services.AddScoped<IJwt, JwtHelper>();
            services.AddScoped<IAWSService, AWSServices>();
            services.AddScoped<IExtractQRCode, ExtractQRCode>();
            services.AddScoped<IPrivateKeyRepo, PrivateKeyRepo>();
            services.AddScoped<IVisitorRepo, VisitorRepo>();
            services.AddScoped<ICredentialCardTypeRepo, CredentialCardTypeRepo>();
            services.AddScoped<INotificationRepo, NotificationRepo>();
            services.AddScoped<INotificationUserRepo, NotificationUserRepo>();
            services.AddScoped<IScheduleUserRepo, ScheduleUserRepo>();
            services.AddScoped<IVisitCardRepo, VisitCardRepo>();
            services.AddScoped<IVisitorSessionImagesRepo, VisitorSessionImagesRepo>();
            services.AddScoped<INotifications, NotificationsService>();
            services.AddSingleton<NotificationHub>();
            services.AddHostedService<VisitStatusUpdaterService>();
            services.AddHostedService<ScheduleUserStatusUpdateService>();
            services.AddHostedService<VisitCardStatusUpdaterService>();
            services.AddSingleton<IDictionary<string, UserConnectionDTO>>(opt => new Dictionary<string, UserConnectionDTO>());


            //Email DI
            services.AddScoped<SecurityGateApv.Domain.Interfaces.EmailSender.IEmailSender, EmailSender.EmailSender>();

            //JWT
            var key = configuration["Jwt:Key"];
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });
            //signalR
            services.AddSignalR( s =>
            {
                s.KeepAliveInterval = TimeSpan.FromSeconds(15);
                s.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
            });
            return services;
        }
        public static WebApplication UseInfras(this WebApplication app)
        {
            var hubConfiguration = new HubConfiguration();
            hubConfiguration.EnableDetailedErrors = true;
            app.MapHub<NotificationHub>("/notificationHub");
            return app;
        }
    }
}
