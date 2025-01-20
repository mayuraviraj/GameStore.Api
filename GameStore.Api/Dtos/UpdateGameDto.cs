using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Dtos;

public record UpdateGameDto(
    [Required][StringLength(20)] string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate);