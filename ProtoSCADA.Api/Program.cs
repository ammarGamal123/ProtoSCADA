using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using ProtoSCADA.Data.Context;
using ProtoSCADA.Data.Interfaces;
using ProtoSCADA.Data.Repositories;
using ProtoSCADA.Entities.Entities;
using ProtoSCADA.Service;
using ProtoSCADA.Service.Abstract;
using ProtoSCADA.Service.Implementation;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.




// add connection string in appsettings.json to program.cs

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RemoteConnection")));




// Dependency Injection setup so that it can be seen in controllers or services
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddScoped(typeof(IUserService), typeof(UserService));
builder.Services.AddScoped(typeof(IAlertService), typeof(AlertService));
builder.Services.AddScoped(typeof(IMachineService), typeof(MachineService));
builder.Services.AddScoped(typeof(IMetricService), typeof(MetricService));
builder.Services.AddScoped(typeof(IFactoryService), typeof(FactoryService));
builder.Services.AddScoped(typeof(IEventService), typeof(EventService));
builder.Services.AddScoped(typeof(IMachineRepository) , typeof(MachineRepository));




// Configure logging - you can choose the log levels you want
builder.Logging.ClearProviders();
builder.Logging.AddConsole();  // Adds Console logging
builder.Logging.AddDebug();    // Adds Debug output


builder.Services.AddControllers();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        // Specify the front-end URL you want to allow (for example, a local Angular app)
        policy.WithOrigins("http://localhost:5261/")  // Adjust with your front-end URL
              .AllowAnyHeader()  // Allow any headers in the request
              .AllowAnyMethod(); // Allow any HTTP methods (GET, POST, PUT, DELETE, etc.)
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseCors("AllowSpecificOrigin");  // Apply CORS globally


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
