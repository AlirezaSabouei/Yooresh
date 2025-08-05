using FluentValidation;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using Yooresh.Application;
using Yooresh.Infrastructure;

namespace Yooresh.API;

public class Program
{
    public static void Main(string[] args)
    {
        IdentityModelEventSource.ShowPII = true;
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();



        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowBlazorClient", policy =>
            {
                policy.WithOrigins("https://localhost:7292/") // your Blazor Server app URL
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials(); // only needed if using cookies or auth
            });
        });




        AddSwagger(builder);
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);

        AddAuthentication(builder);

        builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        var app = builder.Build();

        UseSwagger(app);
        UseExceptionHandler(app);

        app.UseHttpsRedirection();
        app.UseHangfireDashboard();

        // Enable CORS
        app.UseCors("AllowBlazorClient");


        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        // RecurringJob.RemoveIfExists("mytestjob");

        app.Run();
    }

    private static void UseExceptionHandler(WebApplication app)
    {
        app.UseExceptionHandler(builder =>
        {
            builder.Run(async context =>
            {
                var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
                var exception = exceptionHandler?.Error;

                if (exception is ValidationException validationException)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Response.ContentType = "application/json";

                    var errors = validationException.Errors
                        .Select(x => x.ErrorMessage).ToList();

                    await context.Response.WriteAsJsonAsync(errors);
                    return;
                }

                // Generic fallback
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new { error = "An unexpected error occurred." });
            });
        });
    }

    private static void UseSwagger(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        // if (app.Environment.IsDevelopment())

        app.UseSwagger();
        app.UseSwaggerUI();
    }

    private static void AddAuthentication(WebApplicationBuilder builder)
    {
        var key = builder.Configuration["Jwt:Key"]; // put in appsettings.json
        var issuer = builder.Configuration["Jwt:Issuer"];

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine("Token failed: " + context.Exception.Message);
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    Console.WriteLine("Token valid.");
                    return Task.CompletedTask;
                }
            };
        });

        builder.Services.AddAuthorization();
    }

    private static void AddSwagger(WebApplicationBuilder builder)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter your JWT token in the format: Bearer {your token}"
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
                    Array.Empty<string>()
                }
            });
        });
    }
}