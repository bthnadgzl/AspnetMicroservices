using Catalog.API.Model;
using Catalog.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]


public class CatalogController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<CatalogController> _logger;

    public CatalogController(IProductService productService, ILogger<CatalogController> logger)
    {
        
        _productService = productService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> Get()
    {
        var products = await _productService.Get();
        return Ok(products);
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Product>> Get(string id)
    {
        var product = await _productService.Get(id);
        if (product == null)
        {
            _logger.LogError($"Product with id {id}, not found.");
            return NotFound();
        }
        else
        {
            return Ok(product);
        }
    }

    [Route("[action]/{categoryName}")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetByCategory(string categoryName)
    {
        var products = await _productService.GetByCategory(categoryName);

        if (products == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(products);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create([FromBody] Product product)
    {
        await _productService.Create(product);
        return CreatedAtRoute("GetProduct", product.Id , product);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Product product)
    {
        return Ok(await _productService.Update(product));
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        return Ok(await _productService.Delete(id));
    }
}