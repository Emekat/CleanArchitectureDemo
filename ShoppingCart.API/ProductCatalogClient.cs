namespace ShoppingCart.API;
using System.Net.Http.Headers;
using System.Text.Json;

public class ProductCatalogClient: IProductCatalogClient
{
    private readonly HttpClient _client = new HttpClient();
    private static string productCatalogBaseUrl =
      @"https://git.io/JeHiE";
    private static string getProductPathTemplate = "?productIds=[{0}]";

    public ProductCatalogClient(HttpClient client)
    {
         client.BaseAddress = new Uri(productCatalogBaseUrl);
         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _client = client;
    }
    public async Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItemsAsync(int[] productCatalogIds)
    {
        using var response = await RequestProductFromProductCatalag(productCatalogIds);
        return await ConvertToShoppingCartItems(response);

    }

    private async Task<HttpResponseMessage> RequestProductFromProductCatalag(int[] productCatalogIds)
    {
        var productResource = string.Format(getProductPathTemplate, string.Join(",", productCatalogIds));

        return await _client.GetAsync(productResource);
    }

    private static async Task<IEnumerable<ShoppingCartItem>> ConvertToShoppingCartItems(HttpResponseMessage response)
    {
       response.EnsureSuccessStatusCode();
       var products = await JsonSerializer.DeserializeAsync<List<ProductCatalogProduct>>(
          await response.Content.ReadAsStreamAsync(),
          new JsonSerializerOptions
          {
             PropertyNameCaseInsensitive = true
          }) ?? new();
      return products
        .Select(p =>
          new ShoppingCartItem(
            p.ProductId,
            p.ProductName,
            p.ProductDescription,
            p.Price
        ));
    }
 
    private record ProductCatalogProduct(
      int ProductId,
      string ProductName,
      string ProductDescription,
      Money Price);
}