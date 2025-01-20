using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";
    private static readonly List<GameDto> games =
    [
        new(1, "TEst", "Test", 56.56M, new DateOnly())
    ];

    public static RouteGroupBuilder MapEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();
        
        group.MapGet("/", () => games);

        group.MapGet("/{id:int}", (int id) =>
            {
                GameDto? game = games.Find(g => g.Id == id);
                return game is null ? Results.NotFound() : Results.Ok(game);
            })
            .WithName(GetGameEndpointName);

        group.MapPost("/", (CreateGameDto dto) =>
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

        group.MapPut("/{id:int}", (int id, UpdateGameDto updateGameDto) =>
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


        group.MapDelete("/{id:int}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });
        
        return group;
    }
}