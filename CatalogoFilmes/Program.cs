using CatalogoFilmes.Config;
using CatalogoFilmes.Data;
using CatalogoFilmes.Repository;
using CatalogoFilmes.Services.Cache;
using CatalogoFilmes.Services.TMDb;
using CatalogoFilmes.Services.Weather;

var builder = WebApplication.CreateBuilder(args);


// config tipadas
builder.Services.Configure<TmdbOptions>(
    builder.Configuration.GetSection("TMDB"));

builder.Services.Configure<WeatherOptions>(
    builder.Configuration.GetSection("OpenMeteo"));


// servico core
builder.Services.AddMemoryCache();
builder.Services.AddControllersWithViews();


// repositorio (SEM MIGRATIONS)
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();


// servico TMDB
builder.Services.AddScoped<ITmdbApiService, TmdbApiService>();


// servico WEATHER
builder.Services.AddScoped<IWeatherApiService, WeatherApiService>();


// servico de cache personalizado
builder.Services.AddSingleton<ICacheService, MemoryCacheService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Filmes}/{action=Index}/{id?}");

app.Run();