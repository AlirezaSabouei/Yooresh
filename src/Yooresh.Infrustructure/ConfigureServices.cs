using Hangfire;
using Yooresh.Application.Common.Interfaces;
using Yooresh.Infrastructure.EmailTools;
using Yooresh.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yooresh.Domain.Interfaces;
using Yooresh.Infrastructure.JobTools;
using Hangfire.MemoryStorage;
using Yooresh.Application.Common.Tools;
using Yooresh.Infrastructure.PasswordTools;
using Yooresh.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Yooresh.Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<Context>(options =>
        //    options.UseSqlServer(
        //        configuration.GetConnectionString("DefaultConnection"),
        //        builder => builder.MigrationsAssembly(typeof(Context).Assembly.GetName().Name)
        //    ));
        services.AddDbContext<Context>(options => { options.UseInMemoryDatabase("TestDatabase"); });

        services.AddScoped<IContext, Context>();

        var senderEmail = configuration.GetSection("Email")!["Address"]!;
        var senderPassword = configuration.GetSection("Email")!["Password"]!;
        
        services.AddScoped<IEmail>(s=> new Email(senderEmail, senderPassword));

       //services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));
        services.AddHangfire(x => x.UseMemoryStorage());
        services.AddHangfireServer();
        
        services.AddScoped<IJob, Job>();
        services.AddScoped<IPasswordEncryption<BaseEntity>, PasswordEncryption<BaseEntity>>();
        services.AddScoped<IPasswordHasher<BaseEntity>, PasswordHasher<BaseEntity>>();
    }
}