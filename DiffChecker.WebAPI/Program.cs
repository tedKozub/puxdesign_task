using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.MapPost("/api/analyze", (string path) =>
{
    if (string.IsNullOrWhiteSpace(path))
    {
        return Results.BadRequest("Invalid or missing 'path' in the request body.");
    }

    try
    {
        var fullPath = Path.GetFullPath(path);
        if (!Directory.Exists(fullPath))
        {
            return Results.BadRequest("Invalid path");
        }
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
    
    var diff = DiffChecker.DiffChecker.Run(path);
    if (diff == null) return Results.Ok("Initial snapshot created.");
    var json = JsonConvert.SerializeObject(diff, Formatting.None);
    return Results.Ok(json);

});

app.Run();