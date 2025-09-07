using Business;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.StaticFiles;

namespace InternalApi
{
    // Example REST Api
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add Services

            builder.Services.AddControllers();
            builder.Services.AddAuthorization();
            builder.Services.AddOpenApi();
            builder.Services.AddRateLimiter(_ =>
            {
                _.AddFixedWindowLimiter("default", options =>
                {
                    options.PermitLimit = 10;
                    options.Window = TimeSpan.FromSeconds(1);
                    options.QueueLimit = 0;
                });
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                });
            });
            builder.Services.AddSingleton<BasicConfiguration>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                return new BasicConfiguration(configuration);
            });
            builder.Services.AddEndpointsApiExplorer();
            var configuration = builder?.Services?.BuildServiceProvider().GetRequiredService<BasicConfiguration>();
            Configuration.ConfigurationDBContext(builder?.Services!, configuration!.ConnectionString);
            Configuration.ConfigurationRepository(builder?.Services!);
            Configuration.ConfigurationService(builder?.Services!);
            Configuration.ConfigurationAuthentication(builder?.Services!, configuration!.Issuer, configuration!.Key);

            #endregion

            var app = builder!.Build();

            #region App

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = new FileExtensionContentTypeProvider
                {
                    Mappings = { [".svg"] = "image/svg+xml" }
                }
            });
            app.UseHttpsRedirection();
            app.UseRateLimiter();
            app.UseAuthorization();
            app.UseCors("AllowAll");
            app.MapControllers();
            app.UseAuthentication();
            app.UseAuthorization();

            #endregion

            app.Run();
        }
    }
}