using System.Net;
using Basket.API.Entities;
using Basket.API.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;
    private readonly ILogger<BasketController> _logger;

    public BasketController(IBasketService basketService, ILogger<BasketController> logger)
    {
        _basketService = basketService;
        _logger = logger;
    }

    [HttpGet("{userName}", Name = "Get")]
    [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<ShoppingCart>>> Get(string userName)
    {
        var basket = await _basketService.GetBasket(userName);
        return Ok(basket ?? new ShoppingCart(userName));
    }
    

    [HttpPost]
    [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<ShoppingCart>>> Update(ShoppingCart basket)
    {
        await _basketService.UpdateBasket(basket);
        return Ok(basket);
    }

    [HttpDelete("{userName}", Name = "Delete")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(string userName)
    {
        await _basketService.DeleteBasket(userName);
        return Ok();
    }


}