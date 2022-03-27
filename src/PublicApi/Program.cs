using ApplicationCore.Constants;
using FastEndpoints;
using FastEndpoints.Swagger;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using PublicApi.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<AppIdentityDbContext>()
        .AddDefaultTokenProviders();

builder.Services.AddCoreServices();

var configSection = builder.Configuration.GetRequiredSection(BaseUrlConfiguration.CONFIG_NAME);
var baseUrlConfig = configSection.Get<BaseUrlConfiguration>();

var key = Encoding.ASCII.GetBytes(AuthorizationConstants.JWT_SECRET_KEY);
builder.Services.AddAuthentication(config =>
{
    config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

const string CORS_POLICY = "CorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORS_POLICY,
                      builder =>
                      {
                          builder.WithOrigins(baseUrlConfig.WebBase.Replace("host.docker.internal", "localhost").TrimEnd('/'));
                          builder.AllowAnyMethod();
                          builder.AllowAnyHeader();
                      });
});

builder.Services.AddFastEndpoints();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDoc(s => 
{
    s.AddAuth(JwtBearerDefaults.AuthenticationScheme, new()
    {
        Type = OpenApiSecuritySchemeType.Http,
        In = NSwag.OpenApiSecurityApiKeyLocation.Header,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        Description = "Enter a JWT token to authorize the requests..."
    });
});

//protect subscriptionendpoint

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3(c => c.ConfigureDefaults());
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var catalogContext = scopedProvider.GetRequiredService<BookCatalogDbContext>();
        await BookCatalogDbContextSeed.SeedAsync(catalogContext, app.Logger);


        var userManager = scopedProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await AppIdentityDbContextSeed.SeedAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.Run();
