using CycleLog.ApiClient.ApiClients;
using CycleLog.ApiClient.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.HttpOverrides;

namespace CycleLog.MVC
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
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
            }).AddCookie(cookieOptions =>
            {
                cookieOptions.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                cookieOptions.Cookie.HttpOnly = true;
                cookieOptions.Cookie.SameSite = SameSiteMode.Lax;
                cookieOptions.SlidingExpiration = true;

                cookieOptions.ExpireTimeSpan = TimeSpan.FromMinutes(15);
            }).AddOpenIdConnect(options =>
            {
                options.Authority = configuration["OIDC_AUTHORITY"];
                options.ClientId = configuration["OIDC_CLIENT_ID"];
                options.ClientSecret = configuration["OIDC_CLIENT_SECRET"];
                options.ResponseType = "code";
                options.SaveTokens = true;
                options.TokenValidationParameters.ValidateIssuer = true;
                options.UsePkce = true;

                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");
                options.Scope.Add("cyclelog-api-access");

                options.RequireHttpsMetadata = true;
                options.GetClaimsFromUserInfoEndpoint = true;
            });

            builder.Services.AddAuthorization();

            builder.Services.AddScoped<ITrainingSessionApiClient>(apiClient =>
            new TrainingSessionApiClient(configuration["CYCLELOG_API_BASE_URI"]));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Tell ASP.NET to respect forwarded headers from the proxy
            var forwardedOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            };

            // Optional: trust all proxies (be careful in untrusted networks)
            forwardedOptions.KnownNetworks.Clear();
            forwardedOptions.KnownProxies.Clear();

            app.UseForwardedHeaders(forwardedOptions);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
