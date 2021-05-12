namespace ProjectMasters.Games
{
    using System.Collections.Generic;

    public sealed class GameStateService : IGameStateService
    {
        private readonly List<GameState> _gameStates;

        public GameStateService()
        {
            _gameStates = new List<GameState>();
        }

        public IEnumerable<GameState> GetAllGameStates()
        {
            return _gameStates;
        }

        public void AddGameState(string userId)
        {
            var gameState = new GameState { UserId = userId };
            _gameStates.Add(gameState);
        }
    }
}