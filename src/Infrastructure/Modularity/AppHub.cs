using Microsoft.AspNetCore.Routing;

namespace CleanArchitechture.Modularity;

public abstract class AppHub : Light.AspNetCore.Modularity.IModuleEndpoint
{
    public virtual void Map(IEndpointRouteBuilder endpoints)
    { }
}
