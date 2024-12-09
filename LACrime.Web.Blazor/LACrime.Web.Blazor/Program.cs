using LACrimes.Web.Blazor.Client.Pages;
using LACrimes.Web.Blazor.Server.Components;
using LACrimes.EF.Repository;
using LACrimes.Model;
using LACrimes.Web.Blazor.Server.Controllers;
using Microsoft.AspNetCore.Http.Features;
using LACrimes.EF.Context;
using LACrimes.Web.Blazor.Server.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LACrimes.Web.Blazor.Client.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.SessionStorage;

internal class Program {
    private static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddLocalization(options => options.ResourcesPath = "lacrResources");

        builder.Services.AddScoped<IEntityRepo<Area>, AreaRepo>();
        builder.Services.AddScoped<IEntityRepo<Coordinates>, CoordinatesRepo>();
        builder.Services.AddScoped<IEntityRepo<Crime>, CrimeRepo>();
        builder.Services.AddScoped<IEntityRepo<CrimeRecord>, CrimeRecordRepo>();
        builder.Services.AddScoped<IEntityRepo<Premis>, PremisRepo>();
        builder.Services.AddScoped<IEntityRepo<Status>, StatusRepo>();
        builder.Services.AddScoped<IEntityRepo<Street>, StreetRepo>();
        builder.Services.AddScoped<IEntityRepo<SubArea>, SubAreaRepo>();
        builder.Services.AddScoped<IEntityRepo<Victim>, VictimRepo>();
        builder.Services.AddScoped<IEntityRepo<Weapon>, WeaponRepo>();
        builder.Services.AddScoped<IEntityRepo<Account>, AccountRepo>();
        builder.Services.AddScoped<LACrimeDbContext, LACrimeDbContext>();
        builder.Services.AddScoped<CrimeRecordController>();
        builder.Services.AddBlazoredSessionStorage();
        builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        builder.Services.AddAuthorizationCore();
        // Register memory cache
        builder.Services.AddMemoryCache();

        builder.Services.AddRazorPages();
        builder.Services.AddControllersWithViews();

        builder.Services.AddAuthentication(o => {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o => {
            o.RequireHttpsMetadata = false;
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtAuthenticationManager.JWT_SECURITY_KEY)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        builder.Services.AddScoped<UserAccountService>();

        builder.Services.Configure<FormOptions>(options => {
            options.MultipartBodyLengthLimit = 512 * 1024 * 1024; // Set to 512 MB
        });

        // Add CORS policy
        builder.Services.AddCors(options => {
            options.AddPolicy("AllowAllOrigins",
                builder => {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
        });

        // Configure HttpClient with base address
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseUri") ?? "https://localhost:7278/") });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if(app.Environment.IsDevelopment()) {
            app.UseWebAssemblyDebugging();
        } else {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        // Use CORS policy
        app.UseCors("AllowAllOrigins");

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseAntiforgery();

        app.MapControllers();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(LACrimes.Web.Blazor.Client._Imports).Assembly);

        app.Run();
    }
}
