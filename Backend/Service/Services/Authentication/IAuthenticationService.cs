using Topshiriq.Application.DataTransferObjects.Authentication;

namespace Topshiriq.Application.Services.Authentication;

public interface IAuthenticationService
{
    Task<TokenDto> LoginAsync(AuthenticationDto authenticationDto);
}

