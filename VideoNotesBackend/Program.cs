using Microsoft.EntityFrameworkCore;
using MvcICT.Models;
using System.Text.Json.Serialization;
using VideoNotesBackend.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<VideoNotesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VideoNotesContext") ?? throw new InvalidOperationException("Connection string 'MvcICTContext' not found.")));

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
