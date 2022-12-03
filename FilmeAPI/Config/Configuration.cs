using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using FilmeAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace FilmeAPI.Config
{
    public static class Configuration
    {
        public static void Swagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FilmeAPI", Version = "v1" });
            });
        }
        public static void ApiContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FilmeContext>
                (opt => opt.UseSqlServer(configuration
                .GetConnectionString("FilmeConnection")));
        }
        public static void Identity(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<IdentityContext>
                (opt => opt.UseSqlServer(configuration
                .GetConnectionString("IdentityConnection")));


            services.AddIdentity<IdentityUser, IdentityRole>()
                 .AddEntityFrameworkStores<IdentityContext>()
                 .AddDefaultTokenProviders();


            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

            });

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Auth/forbidden";
                options.Cookie.Name = "FilmeApi";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Auth/Unauthorized";
                // ReturnUrlParameter requires 
                //using Microsoft.AspNetCore.Authentication.Cookies;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

        }
    }
}
