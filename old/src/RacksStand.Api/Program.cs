using RacksStand.Api.Extensions;

await WebApplication.CreateBuilder(args)
    .AddServices()
    .ConfigurePipeline()
    .RunAsync();
