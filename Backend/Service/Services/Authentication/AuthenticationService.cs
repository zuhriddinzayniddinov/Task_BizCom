using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Topshiriq.Domain.Exceptions;
using Topshiriq.Application.DataTransferObjects.Authentication;
using Topshiriq.Infrastructure.Authentication;
using Topshiriq.Infrastructure.Repositories.Users;

namespace Topshiriq.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository userRepository;
    private readonly IJwtTokenHandler jwtTokenHandler;
    private readonly IPasswordHasher passwordHasher;
    private readonly JwtOption jwtOptions;

    public AuthenticationService(
        IUserRepository userRepository,
        IJwtTokenHandler jwtTokenHandler,
        IPasswordHasher passwordHasher,
        IOptions<JwtOption> options)
    {
        this.userRepository = userRepository;
        this.jwtTokenHandler = jwtTokenHandler;
        this.passwordHasher = passwordHasher;
        this.jwtOptions = options.Value;
    }

    public async Task<TokenDto> LoginAsync(
        AuthenticationDto authenticationDto)
    {
        var user = await this.userRepository
            .SelectByEmailAsync(authenticationDto.email);

        if (user is null)
        {
            throw new NotFoundException("User with given credentials not found");
        }

        if (!this.passwordHasher.Verify(
                hash: user.PasswordHash,
                password: authenticationDto.password,
                salt: user.Salt))
        {
            throw new ValidationException("Username or password is not valid");
        }

        var updatedUser = await this.userRepository
            .UpdateAsync(user);

        var accessToken = this.jwtTokenHandler
            .GenerateAccessToken(updatedUser);

        return new TokenDto(
            firstName: updatedUser.FirstName,
            role:updatedUser.Role.ToString(),
            accessToken: new JwtSecurityTokenHandler().WriteToken(accessToken),
            expireDate: accessToken.ValidTo);
    }
}