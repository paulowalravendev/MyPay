using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyPay.Application.Abstractions.Authentication;
using MyPay.Application.Abstractions.Clock;
using MyPay.Application.Abstractions.Cryptography;
using MyPay.Application.Abstractions.Data;
using MyPay.Domain.Abstractions;
using MyPay.Domain.Customers;
using MyPay.Infrastructure.Authentication;
using MyPay.Infrastructure.Clock;
using MyPay.Infrastructure.Cryptography;
using MyPay.Infrastructure.Data;
using MyPay.Infrastructure.Outbox;
using MyPay.Infrastructure.Policies;
using MyPay.Infrastructure.Repositories;
using Quartz;
using System.Text;

namespace MyPay.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IPasswordHasher, PasswordHasher>();

        AddPersistence(services, configuration);

        AddBackgroundJobs(services, configuration);

        AddAuthentication(services, configuration);

        return services;
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("Database") ??
            throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionString));
    }

    private static void AddBackgroundJobs(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));
        services.AddQuartz();
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        services.ConfigureOptions<ProcessOutboxMessagesJobSetup>();
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new()
                {
                    ValidIssuer = configuration["Authentication:Issuer"],
                    ValidAudience = configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:Secret"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(
                IdentityData.CustomerPolicy, 
                policy => policy.RequireClaim(IdentityData.CustomerPolicyKey, IdentityData.CustomerPolicyValue));
        });

        services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
    }
}