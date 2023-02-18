using Microsoft.AspNetCore.Mvc;

namespace ShoppingCart.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ShoppingCartController : ControllerBase
{
    private readonly ILogger<ShoppingCartController> _logger;
    private readonly IShoppingCartStore _shoppingCartStore;
    private readonly IProductCatalogClient _productCatalogClient;
    private readonly IEventStore _eventStore;

    public ShoppingCartController(ILogger<ShoppingCartController> logger, IShoppingCartStore shoppingCartStore,
    IProductCatalogClient productCatalogClient, IEventStore eventStore)
    {
        _logger = logger;
        _shoppingCartStore = shoppingCartStore;
        _productCatalogClient = productCatalogClient;
        _eventStore = eventStore;
    }

    [HttpGet("{userId:int}")]
    public ShoppingCart Get(int userId)
    {
       return _shoppingCartStore.Get(userId);
    }

    [HttpPost("{userId:int}/items")]
    public async Task<ShoppingCart> Post(int userId, [FromBody] int[] productIds)
    {
         var shoppingCart = _shoppingCartStore.Get(userId);
         var shoppingCartItems = await _productCatalogClient.GetShoppingCartItemsAsync(productIds);
         shoppingCart.AddItems(shoppingCartItems, _eventStore);
         _shoppingCartStore.Save(shoppingCart);
         return shoppingCart;
    }

    [HttpDelete("{userId:int/items}")]
    public ShoppingCart Delete(int userId, [FromBody] int[] productIds)
    {
       var shoppingCart = _shoppingCartStore.Get(userId);
       shoppingCart.RemoveItems(productIds, _eventStore);
      _shoppingCartStore.Save(shoppingCart);
       return shoppingCart;
    }
}
