using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Yooresh.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Yooresh.Domain.Players;

namespace Yooresh.API.Filters;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IContext _context;

    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock, IContext context)
        : base(options, logger, encoder, clock)
    {
        _context = context;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.Fail("Missing Authorization Header");

        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialsBytes = Convert.FromBase64String(authHeader.Parameter!);
            var credentials = Encoding.UTF8.GetString(credentialsBytes).Split(':');
            var username = credentials[0];
            var password = credentials[1];

            // Perform your authentication logic here, e.g., validate against a database
            var player = await GetPlayer(username, password);
            if (player is {Confirmed: true})
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Email, username),
                    new Claim(ClaimTypes.Role, player.Role.ToString()),
                    new Claim("id",player.Id.ToString())
                };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            if (player is {Confirmed: false})
            {
                return AuthenticateResult.Fail("Account not activated yet");
            }
            return AuthenticateResult.Fail("Invalid username or password");

        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail("Error: " + ex.Message);
        }
    }

    private async Task<Player?> GetPlayer(string username, string password)
    {
        var player = await _context.Players
            .FirstOrDefaultAsync(a => a.Email == username && a.Password == password);

        return player;
    }
}