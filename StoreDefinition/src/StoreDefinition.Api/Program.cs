using StoreDefinition.Application.Extensions;
using StoreDefinition.Infrastructure.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("doc");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
