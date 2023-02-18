using Microsoft.AspNetCore.Mvc;

[Route("/shoppingcart")]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartStore shoppingCartStore;

    public ShoppingCartController(IShoppingCartStore shoppingCartStore)
    {
        this.shoppingCartStore = shoppingCartStore;
    }

    public IActionResult<ShoppingCart> Get(int userId)
    {
         var cart = this.shoppingCartStore.Get(userId);
         return Ok(cart);
    }

}
