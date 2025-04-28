using Microsoft.AspNetCore.Routing;

namespace CleanArchitecture.Modularity;

public abstract class AppHub : Light.AspNetCore.Modularity.IModuleEndpoint
{
    public virtual void Map(IEndpointRouteBuilder endpoints)
    { }
}
