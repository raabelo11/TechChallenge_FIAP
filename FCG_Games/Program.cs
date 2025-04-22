using FGC_Games.Application.Interfaces.GamesInterface;
using FGC_Games.Application.UseCase;
using FGC_Games.Domain.Interface;
using FGC_Games.Infrastructure.Context;
using FGC_Games.Infrastructure.Repository.MongoDbRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ContextMongoDb>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddScoped<ContextMongoDb>();
builder.Services.AddTransient<IGame, GamesRepository>();
builder.Services.AddTransient<IGameCase, GameUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
