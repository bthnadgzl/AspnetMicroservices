using Catalog.API.Model;

namespace Catalog.API.Repository;

public interface IProductService
{
    Task<IEnumerable<Product>> Get();
    Task<Product> Get(string id);
    Task<IEnumerable<Product>> GetByName(string name);
    Task<IEnumerable<Product>> GetByCategory(string categoryName);

    Task Create(Product product);
    Task<bool> Update(Product product);
    Task<bool> Delete(string id);
    

}