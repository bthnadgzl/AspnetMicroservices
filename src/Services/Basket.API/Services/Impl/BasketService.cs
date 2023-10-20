using Basket.API.Entities;
using Basket.API.Repositories;

namespace Basket.API.Services.Impl;

public class BasketService : IBasketService
{
    private readonly IBasketRepository _basketRepository;

    public BasketService(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<ShoppingCart> GetBasket(string userName)
    {
        return await _basketRepository.GetBasket(userName);
    }

    public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
    {
        return await _basketRepository.UpdateBasket(basket);
    }

    public async Task DeleteBasket(string userName)
    {
        await _basketRepository.DeleteBasket(userName);
        
    }
}