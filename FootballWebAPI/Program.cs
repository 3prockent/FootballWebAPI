using FootballWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using FootballWebAPI.Data.CountryData;
using FootballWebAPI.Data.TournamentData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FootballAPIContext>(options => options.UseSqlServer(ConnectionString));
builder.Services.AddScoped<ICountryData, SqlCountryData>();
builder.Services.AddScoped<ITournamentData, SqlTournamentData>();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints =>endpoints.MapControllers());
app.UseAuthorization();

app.MapControllers();

app.Run();
