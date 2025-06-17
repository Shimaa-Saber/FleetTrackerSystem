using AutoMapper;
using FleetTrackerSystem.API.Middlewares;
using FleetTrackerSystem.Application.Background_jops;
using FleetTrackerSystem.AutoMapperProfiles.CompanyPtofiles;
using FleetTrackerSystem.AutoMapperProfiles.UserProfiles;
using FleetTrackerSystem.AutoMapperProfiles.VehicleProfile;
using FleetTrackerSystem.Domain.Interfaces;
using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.Infrastructure.Data;
using FleetTrackerSystem.Infrastructure.Repositories.Repos;
using FleetTrackerSystem.Infrastructure.UnitOfWork;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Diagnostics;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ICompany, CompanyRepository>();
builder.Services.AddScoped<IVehicle, VehicleRepository>();
builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<IAccount, AccountRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<GlobalErrorHandleMiddleWare>();
builder.Services.AddScoped<IVehicleNotifier, VehicleNotifier>();

builder.Services.AddScoped<TransactionMiddleWare>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSwaggerUI", policy =>
    {
        policy.WithOrigins("http://localhost:5000", "https://localhost:5001") // Adjust ports
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddHangfire(config =>
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
          .UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseSqlServerStorage(builder.Configuration.GetConnectionString("CS"))
);

builder.Services.AddHangfireServer();


builder.Logging.ClearProviders();
Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Seq("http://localhost:5341")
            //.WriteTo.MSSqlServer(
                   // connectionString: builder.Configuration.GetConnectionString("CS"),
                   // sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true })
           
            .CreateLogger();

builder.Host.UseSerilog();




builder.Services.AddAutoMapper(typeof(Program).Assembly);



builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("FixedPolicy", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            context.Connection.RemoteIpAddress?.ToString() ?? "unknown", 
            key => new FixedWindowRateLimiterOptions 
            {
                PermitLimit = 5,
                Window = TimeSpan.FromSeconds(10),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            }));
});


builder.Services.AddDbContext<FeetTrackerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CS"))
           .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) 
           .LogTo(log => Debug.WriteLine(log), LogLevel.Information));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddEntityFrameworkStores<FeetTrackerDbContext>()
               .AddDefaultTokenProviders();

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<CompanyProfile>(); 
    cfg.AddProfile<VProfile>();
    cfg.AddProfile<UserProfile>();
});


builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme =
        JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Iss"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Aud"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});




builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation    
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ASP.NET 5 Web API",
        Description = " ITI Projrcy",


    });


    // To Enable authorization using Swagger (JWT)    
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    });
});

builder.Services.AddMediatR(opts =>
               opts.RegisterServicesFromAssembly(typeof(Program).Assembly));


MapperService.Mapper = config.CreateMapper();


var app = builder.Build();
app.UseCors("AllowSwaggerUI");

app.UseMiddleware<GlobalErrorHandleMiddleWare>();
// app.UseMiddleware<TransactionMiddleWare>();

app.UseRateLimiter();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthorization();
app.UseHangfireDashboard();

app.MapControllers();


app.Run();
