using AutoMapper;
using FleetTrackerSystem.AutoMapperProfiles.CompanyPtofiles;
using FleetTrackerSystem.AutoMapperProfiles.UserProfiles;
using FleetTrackerSystem.AutoMapperProfiles.VehicleProfile;
using FleetTrackerSystem.Domain.Data;
using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.Repositories.Interfaces;
using FleetTrackerSystem.Repositories.Repos;
using FleetTrackerSystem.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_Api.Models;
using System.Diagnostics;

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
builder.Services.AddAutoMapper(typeof(Program).Assembly);


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

MapperService.Mapper = config.CreateMapper();


var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    await RoleInitializer.Initialize(services); 
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
