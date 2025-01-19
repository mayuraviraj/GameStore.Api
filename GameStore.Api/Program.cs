using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games =
[
    new(1, "TEst", "Test", 56.56M, new DateOnly())
];

app.MapGet("/games", () => games);
app.MapGet("/games/{id:int}", (int id) => games.FirstOrDefault(g => g.Id == id));
app.MapGet("/", () => "Hello World!");

app.Run();