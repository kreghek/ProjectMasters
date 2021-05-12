using System.Collections.Generic;

namespace ProjectMasters.Games
{
    public interface IGameStateService
    {
        GameState AddGameState(string userId);
        IEnumerable<GameState> GetAllGameStates();
    }
}