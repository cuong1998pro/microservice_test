using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicroTest.Dtos;
using MicroTest.Entities;
using MicroTest.Repositories;
using MicroTest.Service;

namespace MicroTest.Controllers
{
  // url/items
  [ApiController]
  [Route("items")]
  public class ItemsController : ControllerBase
  {
    private readonly ItemsRepository itemsRepository = new();
    [HttpGet]
    public async Task<IEnumerable<ItemDto>> Get()
    {
      var items = (await itemsRepository.GetAllAsync()).Select(item => item.AsDto());
      return items;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetById(Guid id)
    {
      var item = await itemsRepository.GetAsync(id);

      if (item == null)
      {
        return NotFound();
      }
      return item.AsDto();
    }
    [HttpPost]
    public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
    {
      var item = new Item
      {
        Name = createItemDto.Name,
        Description = createItemDto.Description,
        Price = createItemDto.Price,
        CreatedDate = DateTimeOffset.UtcNow,
      };
      await itemsRepository.CreateAsync(item);
      return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }


    [HttpPut]
    public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto)
    {
      var existingItem = await itemsRepository.GetAsync(id);
      if (existingItem == null)
      {
        return NotFound();
      }
      existingItem.Name = updateItemDto.Name;
      existingItem.Description = updateItemDto.Description;
      existingItem.Price = updateItemDto.Price;

      await itemsRepository.UpdateAsync(existingItem);
      return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
      var existingItem = await itemsRepository.GetAsync(id);
      if (existingItem == null)
      {
        return NotFound();
      }
      await itemsRepository.RemoveAsync(existingItem.Id);
      return NoContent();
    }
  }
}