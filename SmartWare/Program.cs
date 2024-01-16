

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using RepositoryUnits;
using Utilities;
using SmartWareData.Models;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;


var builder = WebApplication.CreateBuilder(args);
//builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
//builder.Configuration
//    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//    .AddJsonFile($"appsettings{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
//    .AddEnvironmentVariables();
/************* ADD SERVICES *************/

builder.Services.AddDbContext<SmartwareContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MariaDbConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
// Add cors
builder.Services.AddCors();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));


//builder.Services.Configure<AppSettings>(builder.Configuration);
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
// Business Services
builder.Services.AddScoped<IAuthenticationUnit, AuthenticationUnit>();
builder.Services.AddScoped<IArticlesUnit, ArticlesUnit>();

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
IConfigurationSection appSettingsSection1 = appSettingsSection;

builder.Services.Configure<AppSettings>(appSettingsSection);

//File Logger
//builder.Logging.AddFile(builder.Configuration.GetSection("Logging"));


var app = builder.Build();

/************* CONFIGURE REQUEST PIPELINE *************/

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocumentTitle = "Swagger UI - QuickApp";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "samrtware V1");
       
    });

    //IdentityModelEventSource.ShowPII = true;
}
else
{
    // The default HSTS value is 30 days.
    // You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

/************* SEED DATABASE *************/

using var scope = app.Services.CreateScope();

/************* RUN APP *************/

app.Run();
