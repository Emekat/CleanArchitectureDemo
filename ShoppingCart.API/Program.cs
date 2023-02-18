using Polly;
using Polly.Extensions.Http;
using ShoppingCart.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IShoppingCartStore, ShoppingCartStore>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddHttpClient<IProductCatalogClient, ProductCatalogClient>()
    .AddTransientHttpErrorPolicy(p =>
      p.WaitAndRetryAsync(
        3,
        attempt => TimeSpan.FromMilliseconds(100*Math.Pow(2, attempt))));
// builder.Services.AddHttpClient("productClient").SetHandlerLifetime(TimeSpan.FromMinutes(5))  
//         // important step  
//         .AddPolicyHandler(GetRetryPolicy());

// builder.Services.AddHttpClient("productClient").AddTransientHttpErrorPolicy(p =>
//       p.WaitAndRetryAsync(3,
//         attempt => TimeSpan.FromMilliseconds(100*Math.Pow(2, attempt))));

// IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
// {
//    return HttpPolicyExtensions  
//         // HttpRequestException, 5XX and 408  
//         .HandleTransientHttpError()  
//         // 404  
//         .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)  
//         // Retry two times after delay  
//         .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))  
//         ;  
// }




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
