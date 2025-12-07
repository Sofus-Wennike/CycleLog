using CycleLog.DAL.DAO;
using CycleLog.DAL.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CycleLog.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables()
                .Build();

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = configuration["OIDC_AUTHORITY"];
                    options.Audience = configuration["CYCLELOG_API_CLIENT_ID"];
                    options.RequireHttpsMetadata = true;
                });

            builder.Services.AddAuthorization();

            builder.Services.AddScoped<ITrainingSessionDAO>(DAO =>
            new TrainingSessionDAO(configuration["CONNECTION_STRING"]));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
