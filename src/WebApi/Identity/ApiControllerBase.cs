using Light.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Identity;

[ApiExplorerSettings(GroupName = "Admin")]
public abstract class ApiControllerBase : VersionedApiController;
