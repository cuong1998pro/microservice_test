using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MicroTest.Entities;
using MongoDB.Driver;

namespace MicroTest.Repositories
{
  public class ItemsRepository
  {
    private const string collectionName = "items";
    private readonly IMongoCollection<Item> dbConnection;
    private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;
    public ItemsRepository()
    {
      var mongoClient = new MongoClient("mongodb://localhost:27017");
      var database = mongoClient.GetDatabase("Catalog");
      dbConnection = database.GetCollection<Item>(collectionName);
    }
    public async Task<IReadOnlyCollection<Item>> GetAllAsync()
    {
      return await dbConnection.Find(filterBuilder.Empty).ToListAsync();
    }
    public async Task<Item> GetAsync(Guid id)
    {
      FilterDefinition<Item> filter = filterBuilder.Eq(entity => entity.Id, id);
      return await dbConnection.Find(filter).FirstOrDefaultAsync();
    }
    public async Task CreateAsync(Item entity)
    {
      if (entity != null)
      {
        throw new ArgumentException(nameof(entity));
      }
      await dbConnection.InsertOneAsync(entity);
    }
    public async Task UpdateAsync(Item entity)
    {
      if (entity != null)
      {
        throw new ArgumentException(nameof(entity));
      }
      FilterDefinition<Item> filter = filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
      await dbConnection.ReplaceOneAsync(filter, entity);
    }
    public async Task RemoveAsync(Guid id)
    {
      FilterDefinition<Item> filter = filterBuilder.Eq(entity => entity.Id, id);
      await dbConnection.DeleteOneAsync(filter);
    }
  }
}