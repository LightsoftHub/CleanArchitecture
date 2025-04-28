using Asp.Versioning;

namespace CleanArchitecture.Endpoints;

[ApiVersion("1.0")]
public abstract class VersionedApiController : Light.AspNetCore.Mvc.VersionedApiController;
