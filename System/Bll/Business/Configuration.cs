using Business.Services.Implementations;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Business
{
    public static class Configuration
    {
        public static void ConfigurationDBContext(IServiceCollection collection, string connectionStr)
        {
            EntityGateWay.Configuration.ConfigurationDBContext(collection, connectionStr);
        }

        public static void ConfigurationRepository(IServiceCollection collection)
        {
            EntityGateWay.Configuration.ConfigurationRepository(collection);
        }

        public static void ConfigurationIntegrations(IServiceCollection collection)
        {
            SMTP.Configuration.ConfigurationSMTP(collection);
        }

        public static void ConfigurationService(IServiceCollection collection)
        {
            collection.AddScoped<IUserService, UserService>();
        }

        public static void ConfigurationAuthentication(IServiceCollection collection, string jwtIssuer, string jwtKey)
        {
            collection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
                    };
                });
        }
    }
}