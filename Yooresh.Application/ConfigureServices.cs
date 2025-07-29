using System.Reflection;
using Yooresh.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Yooresh.Application.Villages;
using Yooresh.Application.Common.Tools;
using Yooresh.API.Filters;

namespace Yooresh.Application;

public static class ConfigureServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<VillageFactory, VillageFactory>();

        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(Assembly.GetExecutingAssembly()); // scans for Profile classes
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
           // cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        });

        services.AddMemoryCache();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<ITokenService, TokenService>();
    }
}