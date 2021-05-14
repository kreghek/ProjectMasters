using System.Collections.Generic;

using ProjectMasters.Games;

namespace ProjectMasters.Web.Services
{
    /// <summary>
    /// The storage of all game states.
    /// </summary>
    public interface IGameStateService
    {
        /// <summary>
        /// Creates new game state for sprcified user and stores it.
        /// </summary>
        /// <param name="userId"> Unique user id. </param>
        /// <returns> New created game state. </returns>
        GameState AddGameState(string userId);

        /// <summary>
        /// Gets all stored game states.
        /// </summary>
        /// <returns> Collection of game states. </returns>
        IEnumerable<GameState> GetAllGameStates();
    }
}