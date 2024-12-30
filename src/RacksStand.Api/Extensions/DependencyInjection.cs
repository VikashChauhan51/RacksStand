using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using RacksStand.Api.Core.Infrastructure.Contexts;
using RacksStand.Api.Core.Repositories;
using RacksStand.Api.Core.Services;
using RacksStand.Api.Features.Categories.Domain.Repositories;
using RacksStand.Api.Features.Categories.Domain.Services;
using RacksStand.Api.Features.Products.Domain.Repositories;
using RacksStand.Api.Features.Products.Domain.Services;
using RacksStand.Api.Features.Shops.Domain.Repositories;
using RacksStand.Api.Features.Shops.Domain.Services;
using RacksStand.Api.Features.Suppliers.Domain.Repositories;
using RacksStand.Api.Features.Suppliers.Domain.Services;
using RacksStand.Api.Features.Users.Domain.Entities;
using RacksStand.Api.Features.Users.Domain.Services;
using RacksStand.Persistence.Repositories;
using Serilog;
using Serilog.Events;

namespace RacksStand.Api.Extensions;

public static class DependencyInjection
{

    public static WebApplicationBuilder AddDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<RacksStandContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        builder.Services.AddScoped<DbContext>(sp => sp.GetRequiredService<RacksStandContext>());
        builder.Services.AddScoped<IRacksStandContext>(sp => sp.GetRequiredService<RacksStandContext>());
        builder.Services.AddScoped<IdentityContext>(sp => sp.GetRequiredService<RacksStandContext>());
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        return builder;
    }
    public static WebApplicationBuilder AddApplicationRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IShopRepository, ShopRepository>();
        builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IPlanRepository, PlanRepository>();
        builder.Services.AddScoped<IUserPlanRepository, UserPlanRepository>();
        builder.Services.AddScoped<IUserShopRepository, UserShopRepository>();
        builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();   

        return builder;
    }
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<RacksStandContext>()
        .AddDefaultTokenProviders();


        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IShopService, ShopService>();
        builder.Services.AddScoped<IPlanService, PlanService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IPaymentService, PaymentService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<ISupplierService, SupplierService>();
        return builder;
    }
    public static WebApplicationBuilder AddLogger(this WebApplicationBuilder builder)
    {
        // Configure Serilog
        builder.Host.UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentName()
            .WriteTo.Console()
            .WriteTo.Debug()
            .WriteTo.OpenTelemetry());

        builder.Services.AddOpenTelemetry()
    .ConfigureResource(resourceBuilder =>
        resourceBuilder.AddService(serviceName: builder.Environment.ApplicationName))
    .WithLogging()
    .WithTracing(tracerProviderBuilder =>
        tracerProviderBuilder
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddSource(builder.Environment.ApplicationName))
    .WithMetrics(metricsProviderBuilder =>
        metricsProviderBuilder
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation())
    .UseOtlpExporter();

        return builder;
    }
}
