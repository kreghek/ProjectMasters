using System.Collections.Generic;

namespace ProjectMasters.Games
{
    public interface IGameStateService
    {
        void AddGameState(string userId);
        IEnumerable<GameState> GetAllGameStates();
    }
}