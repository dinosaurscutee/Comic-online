using System.Text;
using MangaOnline.Extensions;
using MangaOnline.Models;
using MangaOnline.Pages.Hubs;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

builder.Services.AddRazorPages().AddRazorPagesOptions(options => { options.RootDirectory = "/Pages"; });
builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<IJwtTokenHandler, JwtTokenHandler>();
builder.Services.AddSingleton<ILogicHandler, LogicHandler>();
builder.Services.AddSingleton<MangaOnlineV1DevPRN221Context>();
builder.Services.AddSingleton<SecurityKey>(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["jwt:key"])));
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapRazorPages();

app.MapHub<NotificationHub>("/hubs/notification");

app.Run();