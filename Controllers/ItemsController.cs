using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MicroTest.Dtos;

namespace MicroTest.Controllers
{
  // url/items
  [ApiController]
  [Route("items")]
  public class ItemsController : ControllerBase
  {
    private static readonly List<ItemDto> items = new()
    {
      new ItemDto(Guid.NewGuid(), "Potion", "test", 5, DateTimeOffset.UtcNow),
      new ItemDto(Guid.NewGuid(), "Antidote", "test", 5, DateTimeOffset.UtcNow),
      new ItemDto(Guid.NewGuid(), "Bronze sword", "test", 5, DateTimeOffset.UtcNow)
    };
    [HttpGet]
    public IEnumerable<ItemDto> Get()
    {
      return items;
    }

    [HttpGet("{id}")]
    public ActionResult<ItemDto> GetById(Guid id)
    {
      var item = items.Where(item => item.ID == id).SingleOrDefault();
      if (item == null)
      {
        return NotFound();
      }
      return item;
    }
    [HttpPost]
    public ActionResult<ItemDto> Post(CreateItemDto createItemDto)
    {
      var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);
      items.Add(item);
      return CreatedAtAction(nameof(GetById), new { id = item.ID }, item);
    }


    [HttpPut]
    public IActionResult Put(Guid id, UpdateItemDto updateItemDto)
    {
      var existsItem = items.Where(item => item.ID == id).SingleOrDefault();
      if (existsItem == null)
      {
        return NotFound();
      }
      var updateItem = existsItem with
      {
        Name = updateItemDto.Name,
        Description = updateItemDto.Description,
        Price = updateItemDto.Price
      };
      var index = items.FindIndex(existsItem => existsItem.ID == id);
      items[index] = updateItem;
      return NoContent();
    } 
    
    [HttpDelete]
    public IActionResult Delete(Guid id)
    {

      var index = items.FindIndex(existsItem => existsItem.ID == id);
      if (index < 0)
      {
        return NotFound();
      }
      items.RemoveAt(index);
      return NoContent();
    } 


  }
}