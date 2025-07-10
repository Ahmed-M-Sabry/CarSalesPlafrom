using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection; 
using CarSales.Infrastructure;
using CarSales.Application;
using CarSales.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services
    .AddDomainDependencies()
    .AddApplicationDependencies()
    .AddInfrastructureDependencies(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Car Sales API V1");
        options.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
