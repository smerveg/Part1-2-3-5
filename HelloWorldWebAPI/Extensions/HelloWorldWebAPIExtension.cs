using HelloWorldWebAPI.Context;
using HelloWorldWebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldWebAPI.Extensions
{
    public static class HelloWorldWebAPIExtension
    {
        public static IServiceCollection AddHelloWorldWebAPIExtension(this IServiceCollection services, IConfiguration Configuration)
        {
            //In-memory
            //services.AddDbContext<ProductContext>(o => o.UseInMemoryDatabase("ProductDB"));

            //MSSQL DB
            services.AddDbContext<ProductContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ProductContext>();

            //Serialization
            services.AddControllers().AddNewtonsoftJson();

            //JWT Configurations
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["AppSettings:ValidIssuer"],
                    ValidAudience = Configuration["AppSettings:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:Secret"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });

            //Cache Profile Configurations
            services.AddControllersWithViews(options =>

                    options.CacheProfiles.Add("ProductCache",new CacheProfile
                    {
                        Duration=60,
                        Location= ResponseCacheLocation.Any
                    })
            );

            //Service registration
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
