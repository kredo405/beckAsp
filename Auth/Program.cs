using Auth.Contracts;
using Auth.Data;
using Auth.Services;
//using JwtAuthManager;
using Microsoft.EntityFrameworkCore;
using JwtTokenHandler = Auth.JwtAuthManager.JwtTokenHandler;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddControllers();
//builder.Services.AddSingleton<JwtTokenHandler>();
builder.Services.AddScoped<JwtTokenHandler>();
//builder.Services.AddScoped<JwtAuthManager.JwtTokenHandler>();
builder.Services.AddTransient<IUserManager,UserManager>();
builder.Services.AddScoped<UserManager>();

builder.Services.AddDbContext<AuthContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnection"));
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.UseAuthorization();
app.MapControllers();
app.Run();