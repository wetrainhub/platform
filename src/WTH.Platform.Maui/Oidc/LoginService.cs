using System.IdentityModel.Tokens.Jwt;
using CommunityToolkit.Mvvm.Messaging;
using IdentityModel.OidcClient;
using WTH.Platform.Maui.Messages;
using WTH.Platform.Maui.Storage;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Maui.Client;

namespace WTH.Platform.Maui.Oidc;

public class LoginService : ILoginService, ITransientDependency
{
    private readonly OidcClient _oidcClient;
    private readonly IStorage _storage;
    private readonly MauiCachedApplicationConfigurationClient _applicationConfigurationClient;

    public LoginService(OidcClient oidcClient, IStorage storage, MauiCachedApplicationConfigurationClient applicationConfigurationClient)
    {
        _oidcClient = oidcClient;
        _storage = storage;
        _applicationConfigurationClient = applicationConfigurationClient;
    }

    public async Task<LoginResult> LoginAsync()
    {
        var loginResult = await _oidcClient.LoginAsync(new LoginRequest());

        if (!loginResult.IsError)
        {
            await SetTokenCacheAsync(loginResult.AccessToken, loginResult.RefreshToken);
            await _applicationConfigurationClient.InitializeAsync();

            WeakReferenceMessenger.Default.Send(new LoginMessage());
        }

        return loginResult;
    }

    public async Task<LogoutResult> LogoutAsync()
    {
        var logoutResult = await _oidcClient.LogoutAsync();
        if (!logoutResult.IsError)
        {
            await ClearTokenCacheAsync();
            await _applicationConfigurationClient.InitializeAsync();

            WeakReferenceMessenger.Default.Send(new LogoutMessage());
        }

        return logoutResult;
    }

    public async Task<string> GetAccessTokenAsync()
    {
        var token = await _storage.GetAsync(PlatformConsts.OidcConsts.AccessTokenKeyName);

        if (!token.IsNullOrEmpty())
        {
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            if (jwtToken.ValidTo <= DateTime.UtcNow)
            {
                var newToken = await TryRefreshTokenAsync();

                if (!newToken.IsNullOrEmpty())
                {
                    return newToken;
                }

                await ClearTokenCacheAsync();
                WeakReferenceMessenger.Default.Send(new LogoutMessage());
            }
        }

        return token;
    }

    public async Task<string> TryRefreshTokenAsync()
    {
        var refreshToken = await _storage.GetAsync(PlatformConsts.OidcConsts.RefreshTokenKeyName);
        if (!refreshToken.IsNullOrEmpty())
        {
            var refreshResult = await _oidcClient.RefreshTokenAsync(refreshToken);
            await SetTokenCacheAsync(refreshResult.AccessToken, refreshResult.RefreshToken);

            return refreshResult.AccessToken;
        }

        return string.Empty;
    }

    private async Task SetTokenCacheAsync(string accessToken, string refreshToken)
    {
        await _storage.SetAsync(PlatformConsts.OidcConsts.AccessTokenKeyName, accessToken);
        await _storage.SetAsync(PlatformConsts.OidcConsts.RefreshTokenKeyName, refreshToken);
    }

    private async Task ClearTokenCacheAsync()
    {
        await _storage.RemoveAsync(PlatformConsts.OidcConsts.AccessTokenKeyName);
        await _storage.RemoveAsync(PlatformConsts.OidcConsts.RefreshTokenKeyName);
    }
}