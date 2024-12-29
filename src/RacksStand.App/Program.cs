using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RacksStand.App;
using System.Net.Http.Headers;
using Refit;
using RacksStand.App.Pages.Shops.Services;
using Polly;
using Blazored.LocalStorage;
using RacksStand.App.Core.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSingleton<ITokenService, TokenService>();

// Register the Refit API client
builder.Services.AddRefitClient<IShopService>()
    .ConfigureHttpClient(async (serviceProvider, client) =>
    {
        // Retrieve the access token from localStorage
        var tokenService = serviceProvider.GetRequiredService<ITokenService>();
        var token = await tokenService.GetAccessTokenAsync();

        // Configure the base URL and Authorization header
        client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    })
    .AddTransientHttpErrorPolicy(policyBuilder =>
        policyBuilder.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2))) 
    .AddTransientHttpErrorPolicy(policyBuilder =>
        policyBuilder.CircuitBreakerAsync(5, TimeSpan.FromSeconds(10))) 
    .AddPolicyHandlerFromRegistry("CustomPolicy");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

