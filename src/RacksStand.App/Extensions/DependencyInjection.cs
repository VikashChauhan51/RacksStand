using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Polly;
using RacksStand.App.Core.Services;
using RacksStand.App.Features.Shops.Services;
using Refit;
using System.Net.Http.Headers;

namespace RacksStand.App.Extensions;

public static class DependencyInjection
{

    public static WebAssemblyHostBuilder AddApplcationServices(this WebAssemblyHostBuilder builder)
    {
        
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


        return builder;
    }
}
