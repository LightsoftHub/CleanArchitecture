using Microsoft.AspNetCore.Builder;

namespace CleanArchitecture.Endpoints;

public abstract class EndpointGroupBase
{
    public abstract void Map(WebApplication app);
}
