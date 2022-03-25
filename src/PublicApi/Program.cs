using FastEndpoints;
using FastEndpoints.Swagger;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http.Json;
using PublicApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);

builder.Services.AddCoreServices();

builder.Services.AddFastEndpoints();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDoc();

//todo add authentication
//protect subscription controller

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3(c => c.ConfigureDefaults());
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseFastEndpoints();

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var catalogContext = scopedProvider.GetRequiredService<BookCatalogDbContext>();
        await BookCatalogDbContextSeed.SeedAsync(catalogContext, app.Logger);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.Run();
