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


app.MapPost("/api/analyze", async (string path) =>
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

    var diffChecker = new DiffChecker.DiffChecker();
    diffChecker.Run(path);
    return Results.Ok();
});

app.Run();