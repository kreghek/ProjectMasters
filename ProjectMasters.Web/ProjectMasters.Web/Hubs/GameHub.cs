namespace ProjectMasters.Web.Hubs
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;

    using DTOs;

    using Games;

    using Microsoft.AspNetCore.SignalR;

    public class GameHub : Hub<IGame>
    {
        private static readonly ConcurrentDictionary<string, string> _userIdDict =
            new ConcurrentDictionary<string, string>();

        private readonly IGameStateService _gameStateService;

        public GameHub(IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }

        public void AssignPersonToLineServer(int lineId, int personId)
        {
            var userId = GetUserIdFromDictionary(Context.ConnectionId);
            var gameState = GetStateByUserId(userId);

            var person = gameState.Team.Persons.FirstOrDefault(p => p.Id == personId);
            var sendLine = gameState.Project.Lines.FirstOrDefault(p => p.AssignedPersons.Contains(person));
            var line = gameState.Project.Lines.FirstOrDefault(l => l.Id == lineId);

            if (line == null)
            {
                return;
            }

            sendLine?.AssignedPersons.Remove(person);
            line.AssignedPersons.Add(person);
            gameState.AssignPerson(line, person);
        }

        public void ChangeUnitPositionsServer(int lineId)
        {
            var userId = GetUserIdFromDictionary(Context.ConnectionId);
            var gameState = GetStateByUserId(userId);

            var lineToGetQueueIndecies = gameState.Project.Lines.SingleOrDefault(x => x.Id == lineId);
            if (lineToGetQueueIndecies is null)
            // Не нашли линию проекта.
            // Это значит, что убили последнего монстра и линия была удалена.
            {
                return;
            }

            var unitPositionInfos = lineToGetQueueIndecies.Units.Select(x => new UnitDto(x)).ToList();
            Clients.Caller.ChangeUnitPositionsAsync(unitPositionInfos);
        }

        public void InitServerState(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("User id can not be empty.");
            }

            // TODO Cleanup the dictionary to prevent overflow with dead connections.
            if (!_userIdDict.TryAdd(Context.ConnectionId, userId))
            {
                throw new InvalidOperationException("Mapping of connection id and user id failed.");
            }

            var gameState = GetStateByUserIdSafe(userId);

            if ((gameState?.Started).GetValueOrDefault())
            {
                var personDtos = gameState.Team.Persons.Select(person => new PersonDto(person)
                {
                    // Получаем линию, которая содержит персонажа.
                    LineId = gameState.Project.Lines.SingleOrDefault(x => x.AssignedPersons.Contains(person))?.Id
                }).ToArray();

                var unitDots = (from line in gameState.Project.Lines from unit in line.Units select new UnitDto(unit))
                    .ToList();

                Clients.Caller.SetupClientStateAsync(personDtos, unitDots);
            }
            else
            {
                gameState = _gameStateService.AddGameState(userId);

                gameState.Started = true;
            }
        }

        public void PreInitServerState(string userId)
        {
            var gameState = GetStateByUserIdSafe(userId);

            if (gameState is null)
            {
                // We have a game state for the user just after he presses the start button.
                // We send false to show start button on the client.
                Clients.Caller.PreSetupClientAsync(false);
                return;
            }

            Clients.Caller.PreSetupClientAsync(gameState.Started);
        }

        public void SendDecision(int number)
        {
            var userId = GetUserIdFromDictionary(Context.ConnectionId);
            var gameState = GetStateByUserId(userId);

            gameState.Player.WaitKeyDayReport = false;
            gameState.Player.WaitForDecision.Choises[number].Apply(gameState);

            gameState.Player.ActiveDecisions = gameState.Player.ActiveDecisions.Skip(1).ToArray();
            if (!gameState.Player.ActiveDecisions.Any())
            {
                gameState.Player.ActiveDecisions = null;
            }

            gameState.Player.WaitForDecision =
                gameState.Player.ActiveDecisions == null ? null : gameState.Player.ActiveDecisions[0];
        }

        private GameState GetStateByUserId(string userId)
        {
            return _gameStateService.GetAllGameStates().Single(x => x.UserId == userId);
        }

        private GameState GetStateByUserIdSafe(string userId)
        {
            return _gameStateService.GetAllGameStates().SingleOrDefault(x => x.UserId == userId);
        }

        private static string GetUserIdFromDictionary(string connectionId)
        {
            if (!_userIdDict.TryGetValue(connectionId, out var userId))
            {
                throw new InvalidOperationException("There is no connection id to map it to user id.");
            }

            return userId;
        }
    }
}