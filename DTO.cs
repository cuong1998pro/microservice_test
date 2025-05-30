using System;
using System.ComponentModel.DataAnnotations;

namespace MicroTest.Dtos
{
  public record ItemDto(Guid ID, string Name, string Description, decimal Price, DateTimeOffset CreatedDate);
  public record CreateItemDto([Required]string Name, string Description, [Range(0,1000)]decimal Price);
  public record UpdateItemDto(string Name, string Description, decimal Price);
}