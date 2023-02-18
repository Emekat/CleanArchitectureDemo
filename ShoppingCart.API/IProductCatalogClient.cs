namespace ShoppingCart.API;
public interface IProductCatalogClient
{
    Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItemsAsync(int[] productIds);
}