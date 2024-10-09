using Microsoft.EntityFrameworkCore;
using Serilog;
using OrgHierarchyAPI.AutoMapper;
using OrgHierarchyAPI.Models;
using OrgHierarchyAPI.Repository;
using OrgHierarchyAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HierarchyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddAutoMapper(typeof(PositionMappingProfile));
//builder.Services.AddScoped<PositionService>();
//serilog
var _loggrer = new LoggerConfiguration()
.ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext()
 //.MinimumLevel.Error()
 //.WriteTo.File("C:\\Project\\Logs\\ApiLog-.log", rollingInterval: RollingInterval.Day)
.CreateLogger();
builder.Logging.AddSerilog(_loggrer);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
