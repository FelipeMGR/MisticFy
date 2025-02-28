using Newtonsoft.Json;
using SpotifyAPI.Web;
using SpotifyAPI;
using MisticFy.Repositories;
using MisticFy.Context;
using Microsoft.EntityFrameworkCore;
using MisticFy.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Configuration.AddJsonFile("appsettings.Development.json");
builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
builder.Services.AddScoped<IMusicRepository, MusicRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISpotifyTokenRefresher, SpotifyTokenRefresher>();
var secretKey = builder.Configuration["JWT:SecretKey"]
    ?? throw new ArgumentException("Invalid secret key!");
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;//Verifica se o token informado é válido
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;//Se o token for inválido, ou não for informado, serão pedidas as credencias do usuário.
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = true, // valida o usuario
        ValidateIssuer = true, // valdia o emissor da chave de segurança
        ValidateLifetime = true, // valida o tempo de vida do token
        ValidateIssuerSigningKey = true, // valida a chave de assinatura do emissor
        ClockSkew = TimeSpan.Zero, // configura o tempo de resposta entre o servidor emissor do token e o receptor
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
}
);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
              .WithExposedHeaders("Authorization"); // Explicitly allow Authorization
    });
});
builder.Services.AddTransient<SpotifyClient>(sp =>
{
    var configuration = builder.Configuration.GetSection("Spotify"); // Obtém as configurações do Spotify
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
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
