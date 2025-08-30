using WebMotors.Endpoints;
using WebMotors.Service;
using WebMotors.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IVehicleService, VehicleService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapGet("/", () => "Hello World!");
app.UseHttpsRedirection();

app.MapVehicleApi();
app.Run();
