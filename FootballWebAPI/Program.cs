using FootballWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using FootballWebAPI.Data.CountryData;
using FootballWebAPI.Data.TournamentData;
using FootballWebAPI.Data.TeamData;
using FootballWebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

string ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FootballAPIContext>(options => options.UseSqlServer(ConnectionString));
builder.Services.AddScoped<ICountryData, SqlCountryData>();
builder.Services.AddScoped<ITournamentData, SqlTournamentData>();
builder.Services.AddScoped<ITeamData, SqlTeamData>();
builder.Services.AddScoped<IFootballerData, SqlFootballerData>();
builder.Services.AddScoped<IMatchData, SqlMatchData>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>endpoints.MapControllers());

app.MapControllers();

app.Run();
