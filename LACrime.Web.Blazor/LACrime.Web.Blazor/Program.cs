using LACrimes.Web.Blazor.Client.Pages;
using LACrimes.Web.Blazor.Server.Components;
using LACrimes.EF.Repository;
using LACrimes.Model;
using LACrimes.Web.Blazor.Server.Controllers;
using Microsoft.AspNetCore.Http.Features;
using LACrimes.EF.Context;

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
        builder.Services.AddScoped<LACrimeDbContext, LACrimeDbContext>();
        builder.Services.AddScoped<CrimeRecordController>();

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        builder.Services.AddControllers();

        builder.Services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 512 * 1024 * 1024; // Set to 512 MB
        });

        builder.Services.AddScoped(sp => {
            return new HttpClient { BaseAddress = new Uri(uriString: builder.Configuration.GetValue<string>("BaseUri") ?? String.Empty) };
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if(app.Environment.IsDevelopment()) {
            app.UseWebAssemblyDebugging();
        } else {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.MapControllers();

        app.UseHttpsRedirection();


        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(LACrimes.Web.Blazor.Client._Imports).Assembly);

        app.Run();
    }
}