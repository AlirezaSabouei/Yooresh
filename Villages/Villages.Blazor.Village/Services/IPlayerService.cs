using Yooresh.Blazor.Village.Models;

namespace Yooresh.Blazor.Village.Services;

public interface IPlayerService
{
    Task<bool> CreatePlayer(CreatePlayerDto createPlayerDto);
}
