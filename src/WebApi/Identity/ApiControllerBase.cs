using Light.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitechture.Identity;

[ApiExplorerSettings(GroupName = "Admin")]
public abstract class ApiControllerBase : VersionedApiController;
