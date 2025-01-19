using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";
List<GameDto> games =
[
    new(1, "TEst", "Test", 56.56M, new DateOnly())
];

app.MapGet("/games", () => games);

app.MapGet("/games/{id:int}", (int id) =>
    {
        GameDto? game = games.Find(g => g.Id == id);
        return game is null ? Results.NotFound() : Results.Ok(game);
    })
    .WithName(GetGameEndpointName);

app.MapGet("/", () => "Hello World!");
app.MapPost("games", (CreateGameDto dto) =>
{
    GameDto game = new(
        games.Count + 1,
        dto.Name,
        dto.Genre,
        dto.Price,
        dto.ReleaseDate
    );
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});

app.MapPut("/games/{id:int}", (int id, UpdateGameDto updateGameDto) =>
{
    var index = games.FindIndex(g => g.Id == id);
    if (index == -1)
    {
        return Results.NotFound();
    }
    games[index] = new GameDto(
        id,
        updateGameDto.Name,
        updateGameDto.Genre,
        updateGameDto.Price,
        updateGameDto.ReleaseDate
    );
    return Results.NoContent();
});


app.MapDelete("/games/{id:int}", (int id) =>
{
    games.RemoveAll(game => game.Id == id);
    return Results.NoContent();
});

app.Run();