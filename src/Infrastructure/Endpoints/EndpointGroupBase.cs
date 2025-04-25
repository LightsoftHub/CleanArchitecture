using Microsoft.AspNetCore.Builder;

namespace CleanArchitechture.Endpoints;

public abstract class EndpointGroupBase
{
    public abstract void Map(WebApplication app);
}
