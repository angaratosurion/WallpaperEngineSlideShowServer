using Microsoft.Extensions.FileProviders;
using WallpaperEngineSlideShowServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAllowSpecificOrigins = "_MyAllowSubdomainPolicy";

 
builder.Services.AddControllersWithViews();
builder.Services.AddDirectoryBrowser();


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

app.UseRouting();
app.UseCors(
       options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
   );

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Wallpaper}/{action=GetImages}/{id?}");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(AppSettingsManager.GeWallPaperPath()),
   RequestPath = "/wallpapers"
});
app.Run();
