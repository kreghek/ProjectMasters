using ProjectMasters.Games;

namespace ProjectMasters.Web.Services
{
    using System.Collections.Generic;

    /// <summary>
    /// Base implemenetation of <see cref="IGameStateService" />.
    /// </summary>
    public sealed class GameStateService : IGameStateService
    {
        private readonly List<GameState> _gameStates;

        public GameStateService()
        {
            _gameStates = new List<GameState>();
        }

        /// <inheritdoc />
        public IEnumerable<GameState> GetAllGameStates()
        {
            return _gameStates;
        }

        /// <inheritdoc />
        public GameState AddGameState(string userId)
        {
            var gameState = new GameState { UserId = userId };
            _gameStates.Add(gameState);

            return gameState;
        }
    }
}