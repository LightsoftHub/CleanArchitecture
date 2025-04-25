using Microsoft.AspNetCore.Authorization;

namespace CleanArchitechture;

public interface IAuthorizationRequirementProvider
{
    IAuthorizationRequirement[] Requirements { get; }
}
