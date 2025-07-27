using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebApp.Services;

public class AuthHeaderHandler : DelegatingHandler
{
    // Ahora depende del AuthenticationStateProvider, no del TokenService
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthHeaderHandler(AuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // Obtenemos el estado de autenticación actual
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        // Si el usuario está autenticado, buscamos nuestro claim del token
        if (user.Identity?.IsAuthenticated == true)
        {
            var token = user.FindFirst("jwt_token")?.Value;
            if (!string.IsNullOrEmpty(token))
            {
                // Añadimos el encabezado de autorización
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}