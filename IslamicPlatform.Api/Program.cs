using AspNetCoreRateLimit;
using IslamicPlatform.Api.Extensions;
using IslamicPlatform.Api.Middleware;
using IslamicPlatform.Application.Interfaces.Services;
using IslamicPlatform.Infrastructure.Data;
using IslamicPlatform.Infrastructure.Data.Seed;
using IslamicPlatform.Infrastructure.Data.Seeders;
using IslamicPlatform.Infrastructure.Sevices;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // =========================
        // Services
        // =========================
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerConfig();
        builder.Services.AddDatabase(builder.Configuration);
        builder.Services.AddIdentityConfig();
        builder.Services.AddJwtConfig(builder.Configuration);
        builder.Services.AddRepositories();
        builder.Services.AddServices();
        builder.Services.AddRedisCache(builder.Configuration);
        builder.Services.AddRateLimiting(builder.Configuration);

        builder.Services.AddHttpClient();
       


        builder.Services.AddCors(option =>
        {
            option.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        var app = builder.Build();

        // =========================
        // DB Migration + Seed
        // =========================
        try
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<DatabaseSeeder>>();
            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var httpClient = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>().CreateClient();

            await context.Database.MigrateAsync();

            await RoleSeeder.SeedRolesAsync(scope.ServiceProvider);

            var seeder = new DatabaseSeeder(context, httpClient, logger, config);
            await seeder.SeedAsync();
        }
        catch (Exception ex)
        {
            // ⚠️ مهم جدًا: لو seed فشل لازم تعرف السبب الحقيقي
            Console.WriteLine("SEED ERROR: " + ex);
        }

        // =========================
        // Middleware Pipeline (Correct Order)
        // =========================

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowAll");
        app.UseIpRateLimiting();    
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseAuthentication();     
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}