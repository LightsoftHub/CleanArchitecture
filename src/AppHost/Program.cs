using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var webApi = builder.AddProject<WebApi>("Web-API");

var clientApp = builder.AddProject<ClientApp>("Client-App");

builder.Build().Run();
