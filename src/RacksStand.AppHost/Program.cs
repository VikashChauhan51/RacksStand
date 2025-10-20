using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var racksStandApp = builder.AddProject<RacksStand_Api>("RacksStandApi");

builder.Build().Run();
