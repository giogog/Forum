﻿using Application;
using Application.Mapping;
using Application.Services;
using Contracts;
using Domain.Entities;
using Domain.Models;
using Infrastructure;
using Infrastructure.DataConnection;
using Infrastructure.Email;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System.Text;

namespace API.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection ConfigureSqlServer(this IServiceCollection services, IConfiguration configuration)=>
            services.AddDbContext<ApplicationDataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), options =>
                {
                    options.MigrationsAssembly("API");
                }));


        public static IServiceCollection ConfigureMongoDb(this IServiceCollection services, IConfiguration configuration) =>
            services.AddScoped<MongoDbContext>(sp =>
            {
                var client = new MongoClient(configuration.GetConnectionString("MongoDbConnection"));
                return new MongoDbContext(client, configuration["MongoDb:DatabaseName"]); ;
            });
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration config)
        {

            var issuer = config.GetValue<string>("ApiSettings:JwtOptions:Issuer");
            var audience = config.GetValue<string>("ApiSettings:JwtOptions:Audience");
            string TokenKey = config["ApiSettings:JwtOptions:Secret"];
            services.AddScoped<CustomJwtBearerEvents>();
            services.AddIdentity<User, Role>(option =>
            {
                option.Password.RequireNonAlphanumeric = false;
                option.User.RequireUniqueEmail = true;


            }).AddEntityFrameworkStores<ApplicationDataContext>().AddDefaultTokenProviders();




            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenKey)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };

                    options.EventsType = typeof(CustomJwtBearerEvents);
                });

            return services;
        }
        public static IServiceCollection GeneralConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<EmailOptions>(config.GetSection("EmailOptions"));
            services.AddTransient<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IEmailSender, EmailSender>();

            return services;
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection service) =>
            service.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureAutomapper(this IServiceCollection services) => 
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
        public static void ConfigureBackGroundService(this IServiceCollection services) =>
            services.AddHostedService<StatusBackgroundService>();

        public static void ConfigureJwtOptions(this WebApplicationBuilder builder) => builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));

        public static void AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {

                options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string example: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                options.AddSecurityRequirement(
                new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new string[] {}
                    }
                });

            });
        }

        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("https://localhost:5003")
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .AllowCredentials());
            });
    }
}
