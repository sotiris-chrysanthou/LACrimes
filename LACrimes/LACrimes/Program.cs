using LACrimes.Client.Pages;
using LACrimes.EF.Repository;
using LACrimes.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IEntityRepo<Area>, AreaRepo>();
builder.Services.AddScoped<IEntityRepo<Street>, StreetRepo>();
builder.Services.AddScoped<IEntityRepo<CrimeRecord>, CrimeRecordRepo>();
builder.Services.AddScoped<IEntityRepo<Crime>, CrimeRepo>();
builder.Services.AddScoped<IEntityRepo<SubArea>, SubAreaRepo>();
builder.Services.AddScoped<IEntityRepo<Status>, StatusRepo>();
builder.Services.AddScoped<IEntityRepo<Weapon>, WeaponRepo>();
builder.Services.AddScoped<IEntityRepo<Victim>, VictimRepo>();
builder.Services.AddScoped<IEntityRepo<Premis>, PremisRepo>();
builder.Services.AddScoped<IEntityRepo<Coordinates>, CoordinatesRepo>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
    app.UseWebAssemblyDebugging();
} else {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();