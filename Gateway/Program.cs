//using Auth.JwtAuthManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
//builder.Services.AddCustomJwtAuthentication();
//builder.Services.AddScoped<JwtTokenHandler>();

var app = builder.Build();

app.MapControllers();
await app.UseOcelot();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
