namespace ShoppingCart.API
{
    public class ShoppingCart
    {
        private readonly HashSet<ShoppingCartItem> items = new HashSet<ShoppingCartItem>();
        public IEnumerable<ShoppingCartItem> Items => items;

        public int UserId { get; }
        public ShoppingCart(int userId) => UserId = userId;
        public void AddItems(IEnumerable<ShoppingCartItem> shoppingCartItems)
        {
            foreach (var item in shoppingCartItems)
                items.Add(item);
        }

        public void AddItems(IEnumerable<ShoppingCartItem> shoppingCartItems, IEventStore eventStore)
        {
            foreach (var item in shoppingCartItems)
            {
                if(items.Add(item))
                {
                    eventStore.Raise("ShoppingCartItemAdded", new {UserId, item});
                }
            }
        }
        public void RemoveItems(int[] productCatalogueIds) => items.RemoveWhere(i => productCatalogueIds.Contains(i.ProductCatalogueId));
        public void RemoveItems(int[] productCatalogueIds, IEventStore eventStore) => items.RemoveWhere(i => productCatalogueIds.Contains(i.ProductCatalogueId));
    }

    public record Money(string Currency, decimal amount);

    public record ShoppingCartItem(int ProductCatalogueId, string ProductName, string Description, Money Price)
    {
        public virtual bool Equals(ShoppingCartItem? obj)
        {
          return obj != null && this.ProductCatalogueId.Equals(obj.ProductCatalogueId);
        }

        public override int GetHashCode()
        {
          return ProductCatalogueId.GetHashCode();
        }
    }
}