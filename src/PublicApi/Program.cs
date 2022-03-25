using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);

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

app.Run();
