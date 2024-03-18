using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Models;
using Net8_MinimalAPIs.Models;
using Net8_MinimalAPIs.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IBookService, BookService>();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the Exception MiddleWare
app.UseStatusCodePages(async statusCodeContext => await Results.Problem(statusCode: statusCodeContext.HttpContext
    .Response.StatusCode)
.ExecuteAsync(statusCodeContext.HttpContext));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();


app.MapGet("/books", (IBookService bookservice) =>

    TypedResults.Ok(bookservice.GetBooks())).WithName("GetBooks")
    .WithOpenApi(s => new Microsoft.OpenApi.Models.OpenApiOperation(s)
    {
        Summary = "Get Library Books",
        Description = "",
        Tags = new List<OpenApiTag> { new() { Name = "Amy's Library Books" } }
    });


app.MapGet("/book/{id}", Results<Ok<Book>, NotFound> (IBookService _bookService, int id) =>
    _bookService.GetBook(id) is { } book
            ? TypedResults.Ok(book) : TypedResults.NotFound()
            ).WithName("GetBookById")
            .WithOpenApi(s => new OpenApiOperation(s)
            {
                Summary = "Get Library Book By Id",
                Description = "Returns information about selected book from the Amy's library.",
                Tags = new List<OpenApiTag> { new() { Name = "Amy's Library" } }
            });




app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
