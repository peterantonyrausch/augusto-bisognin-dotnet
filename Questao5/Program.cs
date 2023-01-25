using MediatR;
using Questao5.Infrastructure.Sqlite;
using System.Reflection;
using Questao5.Extensions;
using Questao5.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// sqlite
builder.Services.AddDatabase(builder.Configuration.GetConnectionString("default"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<BusinessValidationExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
    app.UseHttpsRedirection();

// business validation exception handling
app.UseMiddleware<BusinessValidationExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

// sqlite
app.Services.GetRequiredService<IDatabaseBootstrap>().Setup();

app.Run();

// Informações úteis:
// Tipos do Sqlite - https://www.sqlite.org/datatype3.html