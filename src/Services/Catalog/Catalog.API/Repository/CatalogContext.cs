using Catalog.API.Model;
using MongoDB.Driver;

namespace Catalog.API.Repository;

public class CatalogContext : ICatalogContext
{
    public CatalogContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName")); 
        Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        CatalogContextSeed.SeedData(Products);
    }
    public IMongoCollection<Product> Products { get; }

    public async Task<IEnumerable<Product>> Get()
    {
        return await Products
            .Find(p => true)
            .ToListAsync();
    }
    public async Task<Product> Get(string id)
    {
        return await Products
                        .Find(p => p.Id.Equals(id))
                        .FirstOrDefaultAsync();

    }

    public async Task<IEnumerable<Product>> GetByName(string name)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

        return await Products
                        .Find(filter)
                        .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByCategory(string categoryName)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

        return await Products
                        .Find(filter)
                        .ToListAsync();
    }

    public async Task Create(Product product)
    {
        await Products
                .InsertOneAsync(product);
    }

    public async Task<bool> Update(Product product)
    {
        var updatedResult =  await Products
                                    .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement:product);

        return updatedResult.IsAcknowledged
               && updatedResult.ModifiedCount > 0;
    }

    public async Task<bool> Delete(string id)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id,id);

        DeleteResult deleteResult = await Products
                                            .DeleteOneAsync(filter);

        return deleteResult.IsAcknowledged
               && deleteResult.DeletedCount > 0;
    }
    
    
}