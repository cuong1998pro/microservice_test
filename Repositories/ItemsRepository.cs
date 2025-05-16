using MicroTest.Entities;
using MongoDB.Driver;

namespace MicroTest.Repositories
{
  public class ItemsRepository
  {
    private const string collectionString = "";
    private readonly IMongoCollection<Item> dbConnection;
    private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;
    public ItemsRepository {
      var mongoClient = new MongoClient("mongodb://localhost:")
    }
  }
}