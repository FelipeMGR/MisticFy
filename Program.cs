using Newtonsoft.Json;
using SpotifyAPI.Web;
using SpotifyAPI;
using MisticFy.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Configuration.AddJsonFile("appsettings.Development.json");
builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
