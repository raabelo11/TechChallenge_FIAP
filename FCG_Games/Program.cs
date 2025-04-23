using FGC_Games.Application.Interfaces.GamesInterface;
using FGC_Games.Application.UseCase;
using FGC_Games.Domain.Interface;
using FGC_Games.Infrastructure.Configurations;
using FGC_Games.Infrastructure.Context;
using FGC_Games.Infrastructure.Repository.MongoDbRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Configurar Mongo DbSettings
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
//Registrar o Contexto MongoDb
builder.Services.AddSingleton<ContextMongoDb>();
//Registrar o Repositorio MongoDb
builder.Services.AddTransient<IMongoRepository, RepositoryMongo>();
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
