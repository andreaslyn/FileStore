var builder = WebApplication.CreateBuilder(args);

var policyName = "FileStorePolicy";
var originUrl = "https://localhost";

// Add services to the container.

builder.Services.AddCors(options =>
  options.AddPolicy(policyName,
    builder => builder.WithOrigins(originUrl).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
  )
);

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(policyName);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;

app.Run();
