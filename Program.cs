using SpotifyAPI.Web;
using MisticFy.Repositories;
using MisticFy.Context;
using MisticFy.src.Middleware;
using MisticFy.DTO;
using MisticFy.src.Services;
using Microsoft.EntityFrameworkCore;
using MisticFy.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Scalar.AspNetCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("MistFy_v1", new OpenApiInfo { Title = "MisticFy", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT no formato: Bearer {seu_token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


builder.Services.AddControllers(); 
builder.Services.AddHttpContextAccessor();
builder.Configuration.AddJsonFile("appsettings.Development.json");
builder.Services.AddAutoMapper(typeof(SpotifyProfile));
builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
builder.Services.AddScoped<ISpotifyService, SpotifyService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISpotifyTokenRefresher, SpotifyTokenRefresher>();
var secretKey = builder.Configuration["JWT:SecretKey"]
    ?? throw new ArgumentException("Invalid secret key!");
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
}
);


builder.Services.AddTransient<SpotifyClient>(sp =>
{
    var configuration = builder.Configuration.GetSection("Spotify");
    var clientId = configuration["ClientId"];
    var clientSecret = configuration["ClientSecret"];


    if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
    {
        throw new InvalidOperationException("ClientId and ClientSecret must be provided.");
    }

    var config = SpotifyClientConfig.CreateDefault();
    var request = new ClientCredentialsRequest(clientId, clientSecret);
    var response = new OAuthClient(config).RequestToken(request).Result;

    var token = new SpotifyClient(config.WithToken(response.AccessToken));
    return token;
});
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSwaggerUI", policy =>
    {
        policy.WithOrigins("http://localhost:5092")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "swagger/{documentName}/swagger.json";
    });
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("swagger/MistFy_v1/swagger.json", "MistFy_v1");
    });
}
app.UseCors();
app.UseAuthentication();
app.UseMiddleware<SpotifyAuthMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();
