using RacksStand.Api;

await WebApplication.CreateBuilder(args)
    .AddServices()
    .ConfigurePipeline()
    .RunAsync();
