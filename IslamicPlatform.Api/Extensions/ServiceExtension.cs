using AspNetCoreRateLimit;
using IslamicPlatform.Application.Interfaces.Services;
using IslamicPlatform.Application.Services;
using IslamicPlatform.Domain.Entites.Identity;
using IslamicPlatform.Domain.Interfaces.Repositories;
using IslamicPlatform.Domain.Interfaces.Repositories.Audio;
using IslamicPlatform.Domain.Interfaces.Repositories.Azkar;
using IslamicPlatform.Domain.Interfaces.Repositories.hadith;
using IslamicPlatform.Domain.Interfaces.Repositories.Identity;
using IslamicPlatform.Domain.Interfaces.Repositories.Quran;
using IslamicPlatform.Infrastructure.Data;
using IslamicPlatform.Infrastructure.Repositorys;
using IslamicPlatform.Infrastructure.Repositorys.Audio;
using IslamicPlatform.Infrastructure.Repositorys.Azkar;
using IslamicPlatform.Infrastructure.Repositorys.hadith;
using IslamicPlatform.Infrastructure.Repositorys.Identity;
using IslamicPlatform.Infrastructure.Repositorys.Quran;
using IslamicPlatform.Infrastructure.Sevices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Runtime.CompilerServices;
using System.Text;

namespace IslamicPlatform.Api.Extensions
{
    public static class ServiceExtension
    {

      
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            return services;
        }

        public static IServiceCollection AddIdentityConfig(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            return services;
        }

        public static IServiceCollection AddJwtConfig(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
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
                    ValidIssuer = config["JWT:Issuer"],
                    ValidAudience = config["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(config["JWT:Key"]!))
                };
            });
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISurahRepository, SurahRepository>();
            services.AddScoped<IAyahRepository, AyahRepository>();
            services.AddScoped<ISheikhRepository, SheikhRepository>();
            services.AddScoped<IZikrRepository, ZikrRepository>();
            services.AddScoped<IHadithRepository, HadithRepository>();
            services.AddScoped<IBookmarkRepository, BookmarkRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IReadingProgressRepository, ReadingProgressRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IQuranService, QuranService>();
            services.AddScoped<IAudioService, AudioService>();
            services.AddScoped<IAzkarService, AzkarService>();
            services.AddScoped<IHadithService, HadithService>();
            services.AddScoped<IBookmarkService, BookmarkService>();
            services.AddScoped<IReadingProgressService, ReadingProgressService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
           // services.AddScoped<ICacheService, CacheService>();
        //    services.AddScoped<IAIAssistantSevices, AIAssistantSevices>();
            services.AddHttpClient<IAIAssistantSevices, AIAssistantSevices>();

            services.AddHttpClient<IPrayerTimesService, PrayerTimesService>();
            return services;
        }

        public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration config)

        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = config.GetConnectionString("Redis");
                options.InstanceName = "IslamicPlatform:";
            });
            services.AddScoped<ICacheService, CacheService>();
            return services;
        }


        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Islamic Platform API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "ادخل الـ JWT token بس من غير كلمة Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id   = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
            });
            return services;
        }


        public static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration config)
        {
            services.AddMemoryCache();
            // بيستخدم RAM لتخزين عدد الطلبات والـ IPs

            services.Configure<IpRateLimitOptions>(config.GetSection("IpRateLimiting"));
            // بيقرأ إعدادات الـ Rate Limiting من appsettings.json

            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            // بيخزن قواعد (Rules) الـ Rate Limiting لكل IP في الذاكرة

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            // بيعد عدد الطلبات لكل IP ويحفظه في الذاكرة

            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            // بيشغل ويظبط إعدادات نظام الـ Rate Limiting كله

            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            // بيحدد طريقة التعامل مع الطلبات المتزامنة (Concurrent Requests)

            services.AddInMemoryRateLimiting();
            // بيفعل نظام الـ Rate Limiting باستخدام Memory (مش Database)

            return services;
            // بيرجع services عشان نكمل chaining في Program.cs
        }




    }
}
