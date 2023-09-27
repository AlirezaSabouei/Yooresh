using Yooresh.Application.Common.Interfaces;
using Yooresh.Domain.Entities.Players;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Yooresh.Application.Players.Commands;

public record CreatePlayerCommand : IRequest<bool>
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PasswordConfirmation { get; set; } = null!;
    public string? SiteAddress { get; set; }
}

public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, bool>
{
    private readonly IContext _context;
    private readonly IEmail _email;
    private readonly IConfiguration _configuration;

    public CreatePlayerCommandHandler(IContext context, IEmail email,IConfiguration configuration)
    {
        _context = context;
        _email = email;
        _configuration = configuration;
    }

    public async Task<bool> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = new Player(request.Name, request.Email, request.Password, Role.SimplePlayer);
        _context.Players.Add(player);
        await _context.SaveChangesAsync(cancellationToken);

        var message = _configuration.GetSection("Email")["Message"];
        message = message!
            .Replace("@link", request.SiteAddress)
            .Replace("@guid", player.Id.ToString());

        await _email.SendEmail(player.Email, "Activate Your Account", message);
        return true;
    }
}