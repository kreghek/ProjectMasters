namespace ProjectMasters.Web.Hubs
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using DTOs;

    using Games;

    using Microsoft.AspNetCore.SignalR;

    using Services;
    using Services;

    public class GameHub : Hub<IGame>
    {
        private readonly IGameStateService _gameStateService;
        private readonly IUserManager _userManager;

        public GameHub(IGameStateService gameStateService, IUserManager userManager)
        {
            _gameStateService = gameStateService;
            _userManager = userManager;
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

            _userManager.AddUserConnection(Context.ConnectionId, userId);

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

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception).ConfigureAwait(false);

            _userManager.RemoveUserConnection(Context.ConnectionId);
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

            gameState.Player.WaitForDecision = gameState.Player.ActiveDecisions?[0];
        }

        private GameState GetStateByUserId(string userId)
        {
            return _gameStateService.GetAllGameStates().Single(x => x.UserId == userId);
        }

        private GameState GetStateByUserIdSafe(string userId)
        {
            return _gameStateService.GetAllGameStates().SingleOrDefault(x => x.UserId == userId);
        }

        private string GetUserIdFromDictionary(string connectionId)
        {
            var userId = _userManager.GetUserIdByConnectionId(connectionId);

            return userId;
        }
    }
}