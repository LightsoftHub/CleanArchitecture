using Light.Identity;

namespace CleanArchitecture.Identity;

public interface ILoginService
{
    Task<IResult<TokenDto>> GetTokenAsync(string username, string password);
}
