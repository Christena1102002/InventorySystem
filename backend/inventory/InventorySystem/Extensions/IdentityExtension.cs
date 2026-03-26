using InventorySystem.Application.Interfaces;
using InventorySystem.Domain.Entities;
using InventorySystem.Infrastracture.Data;
using InventorySystem.Infrastracture.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace InventorySystem.API.Extensions
{
        public static class IdentityExtension
        {
            public static IServiceCollection AddIdentityService(this IServiceCollection Services, IConfiguration Configuration)
            {
                Services.AddScoped<ITokenService, JwtTokenService>();
                // 2)Identity
                Services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<InventoryDbContext>()
                    .AddDefaultTokenProviders();

                Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = Configuration["JwtSettings:Issuer"],
                      ValidAudience = Configuration["JwtSettings:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(
                          Encoding.UTF8.GetBytes(Configuration["JwtSettings:Secret"]))
                  };
              });


                Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                    // تعريف الـ JWT Bearer
                    var securityScheme = new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Description = "Enter the token in this format: 'Bearer {token}'",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    };
                    c.EnableAnnotations();
                    c.AddSecurityDefinition("Bearer", securityScheme);

                    // إضافة الـ requirement بحيث كل الـ endpoints اللي محمية بـ [Authorize] تستخدم الـ token
                    var securityRequirement = new OpenApiSecurityRequirement
                    {
                        { securityScheme, new string[] { } }
                    };

                    c.AddSecurityRequirement(securityRequirement);
                });

                return Services;
            }
        }
    }


