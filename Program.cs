using Microsoft.EntityFrameworkCore;
using WorkoutApplication.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddDbContext<DataContext>(OptionsBuilderConfigurationExtensions=>
//OptionsBuilderConfigurationExtensions.UseInMemoryDatabase("WorkoutDB"));

builder.Services.AddDbContext<DataContext>(OptionsBuilderConfigurationExtensions=>
OptionsBuilderConfigurationExtensions.UseNpgsql(builder.Configuration.GetConnectionString("default")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using(var scope=((IApplicationBuilder)app).ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
using(var context=scope.ServiceProvider.GetService<DataContext>()) context?.Database.EnsureCreated();
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
