using SoundPalette.Api.Models;
using SoundPalette.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services for dependency injection
builder.Services.AddScoped<IColorParser, ColorParser>();
builder.Services.AddScoped<IMusicTheoryService, MusicTheoryService>();
builder.Services.AddScoped<IChordBuilder, ChordBuilder>();

var app = builder.Build();

// Map the POST /convert endpoint
app.MapPost("/convert", (ColorInputModel input, IColorParser colorParser, IMusicTheoryService musicTheory, IChordBuilder chordBuilder) =>
{
    // Parse HEX to HSL
    var hsl = colorParser.ParseToHsl(input.ColorValue);
    // Map HSL to musical properties
    var pitchClass = musicTheory.GetPitchClassFromHue(hsl.H);
    var mode = musicTheory.GetCombinedModeAndExtension(hsl.S, hsl.L);
    // Build MIDI notes and frequencies
    var (midiNotes, frequencies) = chordBuilder.BuildMusicalProperties(pitchClass, mode);
    // Return the output model
    return Results.Ok(new ChordOutputModel(
        input.ColorValue,
        hsl,
        pitchClass,
        mode,
        midiNotes,
        frequencies
    ));
});

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
    var forecast =  Enumerable.Range(1, 5).Select(index =>
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

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
