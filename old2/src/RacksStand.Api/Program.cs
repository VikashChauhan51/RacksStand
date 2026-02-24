using RacksStand.Api.Core.Extensions;

await WebApplication.CreateBuilder(args)
  .ConfigureServices()
  .ConfigurePipelines()
  .RunAsync();
