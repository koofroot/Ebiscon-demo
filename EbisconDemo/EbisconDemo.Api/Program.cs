using EbisconDemo.Api.Extensions;
using EbisconDemo.Api.HostedServices;
using EbisconDemo.Api.Mapping;
using EbisconDemo.Infrastructure.DI;
using EbisconDemo.Services.Constants;
using EbisconDemo.Services.Models.Configuration;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen((opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Ebiscon API", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", GetSecurityScheme());
    opt.AddSecurityRequirement(GetSecurityRequirement());
}));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(SetupJwtOptions);

builder.Services.AddHttpClient();
builder.Services.AddServices();
builder.Services.AddData();
builder.Services.AddAutoMapper();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddModelValidation();

builder.Services.ConfigureSetting<ProductSources>(builder.Configuration);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddContext(connectionString);

builder.Services.AddHostedService<DailyProductSyncService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseServerErrorMiddleware();

app.MapControllers();

app.Run();

static void SetupJwtOptions(JwtBearerOptions options)
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = EbisconConstants.JwtIssuer,
        ValidAudience = EbisconConstants.JwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EbisconConstants.JwtSecret))
    };
}

static OpenApiSecurityScheme GetSecurityScheme()
{
    return new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Please enter a valid token"
    };
}

static OpenApiSecurityRequirement GetSecurityRequirement()
{
    return new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        };
}