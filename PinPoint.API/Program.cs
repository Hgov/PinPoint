using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog;
using PinPoint.API;
using PinPoint.API.Middleware;
using PinPoint.DataAccess.Helpers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiDatabase")));
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddAntiforgery(options => options.HeaderName = "__RequestVerificationToken");

// Imapper: Add services to the Profiles.
builder.Services.ConfigureMapperService();

// NLog: Setup NLog for Dependency injection
//builder.Logging.ClearProviders();
//builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
//builder.Host.UseNLog();
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/Configuration/nlog.config"));
builder.Services.ConfigureLoggerService();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "PinPoint API",
        Description = "An ASP.NET Core Web API for managing PinPoint items",
        TermsOfService = new Uri("https://localhost:7098/api"),
        Contact = new OpenApiContact
        {
            Name = "Example User",
            Url = new Uri("https://localhost:7098/api/user")
        },
        //License = new OpenApiLicense
        //{
        //    Name = "Example License",
        //    Url = new Uri("https://example.com/license")
        //}
    });
    //options.AddSecurityDefinition("ApiKey",new OpenApiSecurityScheme
    //{
    //    Description = "ApiKey must appear in header",
    //    Type = SecuritySchemeType.ApiKey,
    //    Name = "XApiKey",
    //    In = ParameterLocation.Header,
    //    Scheme = "ApiKeyScheme"
    //});
    //var key = new OpenApiSecurityScheme()
    //{
    //    Reference = new OpenApiReference
    //    {
    //        Type = ReferenceType.SecurityScheme,
    //        Id = "ApiKey"
    //    },
    //    In = ParameterLocation.Header
    //};
    //var requirement = new OpenApiSecurityRequirement
    //                {
    //                         { key, new List<string>() }
    //                };
    //options.AddSecurityRequirement(requirement);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "PinPoint API v1");
    });

}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
//app.UseApiKeyMiddleware();
app.MapControllers();
app.UseResponseWrapperMiddleware();

app.Run();




