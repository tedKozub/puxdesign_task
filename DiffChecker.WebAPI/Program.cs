using DiffChecker.API;

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


app.MapPost("/api/analyze", async (AnalyzeRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Path))
    {
        return Results.BadRequest("Invalid or missing 'path' in the request body.");
    }

    try
    {
        var fullPath = Path.GetFullPath(request.Path);
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
    diffChecker.Run(request.Path);
    return Results.Ok();
});

app.Run();