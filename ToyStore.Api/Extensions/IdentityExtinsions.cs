using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToyStore.Core.Models;
using ToyStore.Infrastructure.Data;

namespace ToyStore.Api.Extensions
{
    public static class IdentityExtinsions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration) {
            
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("ToyDb")));
            services.AddIdentity<AppUser, IdentityRole>(
              opt =>
              {
                  opt.Password.RequireNonAlphanumeric = false;
                  opt.Password.RequiredLength = 6;
                  opt.Password.RequireLowercase = false;
                  opt.Password.RequireUppercase = false;
                  opt.Password.RequireDigit = false;
                  opt.SignIn.RequireConfirmedEmail = false;
              }
          )   //.AddRoleManager<RoleManager<IdentityRole>>()
              .AddEntityFrameworkStores<AppDbContext>()
              .AddSignInManager<SignInManager<AppUser>>()
          .AddUserManager<UserManager<AppUser>>()
              .AddDefaultTokenProviders();

            var secretKey = configuration.GetSection("JwtSetting:JwtKey").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(secretKey));

            services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(op =>
               {
                   op.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidateIssuerSigningKey = true,
                       ValidateIssuer = true,
                       ValidIssuer = configuration.GetSection("JwtSetting:Issuer").Value,
                       ValidateAudience = false,
                       IssuerSigningKey = key
                   };
               });

            return services;
        }
    }
}
