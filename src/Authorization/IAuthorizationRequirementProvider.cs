using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture;

public interface IAuthorizationRequirementProvider
{
    IAuthorizationRequirement[] Requirements { get; }
}
