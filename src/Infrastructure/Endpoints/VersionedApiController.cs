using Asp.Versioning;

namespace CleanArchitechture.Endpoints;

[ApiVersion("1.0")]
public abstract class VersionedApiController : Light.AspNetCore.Mvc.VersionedApiController;
