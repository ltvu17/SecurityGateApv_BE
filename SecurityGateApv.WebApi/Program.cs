
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SecurityGateApv.Application.Common;
using SecurityGateApv.Application.DTOs.Req.Validators;
using SecurityGateApv.Application.Extensions;
using SecurityGateApv.Application.Services;
using SecurityGateApv.Application.Services.Interface;
using SecurityGateApv.Infras.Extentions;
using SecurityGateApv.WebApi.Query;
using System.Reflection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;


namespace SecurityGateApv.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetSection("ConnectionStrings").Value;
            // Add services to the container.
            builder.Services.AddInfrastructure(connectionString, builder.Configuration);
            builder.Services.AddServices();
            builder.Services.AddGraphQLServer()
                .AddFiltering()
                .AddSorting()
                .AddQueryType<QueryExample>();
            //.AddTypeExtension<VisitorSessionQuery>();



            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test01", Version = "v1" });

                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme."

                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
                }
                );

            builder.Services.Configure<MvcOptions>(options =>
            {
                options.ModelMetadataDetailsProviders.Add(
                    new SystemTextJsonValidationMetadataProvider());
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseCors(builder => builder
               .AllowAnyHeader()
               .AllowAnyMethod()
               .SetIsOriginAllowed(_ => true)
               .AllowCredentials());
            app.MapControllers();
            app.MapGraphQL();
            app.UseWebSockets();
            app.UseInfras();

            app.Run();
        }
    }
}
