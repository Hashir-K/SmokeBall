using Microsoft.Extensions.Primitives;
using SmokeBall.BusinessLogic.Services.Implementations;
using SmokeBall.BusinessLogic.Services.Interfaces;

const string SEARCH_URL_ENDPOINT = "SearchEndPoint";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//inject services
var searchUri = builder.Configuration.GetValue<string>(SEARCH_URL_ENDPOINT);
builder.Services.AddSingleton<IKeywordSearchService>(new GoogleSearchService(searchUri));

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
