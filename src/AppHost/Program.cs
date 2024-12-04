using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var webApi = builder.AddProject<WebApi>("Web-API");

builder.Build().Run();
