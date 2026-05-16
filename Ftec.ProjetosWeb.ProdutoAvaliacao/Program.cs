using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Avaliação de Produtos",
        Version = "v1",
        Description = "Microserviço responsável pelo gerenciamento de avaliações de produtos."
    });
});

var app = builder.Build();

// Swagger UI
app.UseSwagger();

app.UseSwaggerUI();



app.UseAuthorization();

app.MapControllers();

app.Run();