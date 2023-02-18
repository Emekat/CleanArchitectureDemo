namespace ShoppingCart.API;
public interface IShoppingCartStore
{
    ShoppingCart Get(int userId);
    void Save(ShoppingCart shoppingCart);
}