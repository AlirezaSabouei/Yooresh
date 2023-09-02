using Eloy.API.Filters;
using Eloy.Infrastructure.Persistence;
using Eloy.Application;
using Eloy.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Eloy.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Add Database
        //  builder.Services.AddDbContext<Context>(options =>
        //  options.UseSqlServer(
        //   builder.Configuration.GetConnectionString("DefaultConnection"),
        //    a => a.MigrationsAssembly(typeof(Mafia.Infrastructure.Migrations.Add_Role_Table).GetTypeInfo().Assembly.GetName().Name)
        //   ));
        builder.Services.AddDbContext<Context>(options => { options.UseInMemoryDatabase("TestDatabase"); });

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            // Add the Basic Authorization button
            c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "basic",
                Description = "Basic Authorization header using the Bearer scheme.",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "basic"}
                    },
                    new string[] { }
                }
            });
        });

        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);

        builder.Services.AddAuthentication("BasicAuthentication")
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        // if (app.Environment.IsDevelopment())
        // {
        app.UseSwagger();
        app.UseSwaggerUI();
        // }

        app.UseHttpsRedirection();


       // app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}