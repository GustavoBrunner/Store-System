using Microsoft.EntityFrameworkCore;
using products_backend.Models.Context;

var builder = WebApplication.CreateBuilder(args);

//ignore cycle references
builder.Services.AddControllers();//AddJsonOptions(options => 
//     options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//permite requisições de qualquer origem
builder.Services.AddCors();

var connectionString = builder.Configuration
    .GetConnectionString(nameof(StoreDbContext));

builder.Services.AddDbContext<StoreDbContext>( options => {
    options.UseMySql(connectionString,
    ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//adiciona a liberação para requisições de qualquer origem, para qualquer método, e com qualquer cabeçalho
app.UseCors(options =>
    options.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.Run();
