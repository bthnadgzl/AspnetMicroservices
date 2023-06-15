using Catalog.API.Model;
using Catalog.API.Repository;
using MongoDB.Driver;

namespace Catalog.API.Repositories.impl;

public class ProductService : IProductService
{
    private readonly ICatalogContext _context;

    public ProductService(ICatalogContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Product>> Get()
    {
        return await _context.Get();
    }

    public async Task<Product> Get(string id)
    {
        return await _context.Get(id);

    }

    public async Task<IEnumerable<Product>> GetByName(string name)
    {

        return await _context.GetByName(name);
    }

    public async Task<IEnumerable<Product>> GetByCategory(string categoryName)
    {
        return await _context.GetByCategory(categoryName);
    }

    public async Task Create(Product product)
    {
        await _context.Create(product);
    }

    public async Task<bool> Update(Product product)
    {
        return await _context.Update(product);
    }

    public async Task<bool> Delete(string id)
    {
        return await _context.Delete(id);
    }
}