namespace ProjectMasters.Web.Hubs
{
    using System.Linq;

    using Assets.BL;

    using DTOs;

    using Games;

    using Microsoft.AspNetCore.SignalR;

    public class GameHub : Hub<IGame>
    {
        private readonly IGameStateService _gameStateService;
        private string userId = "test";

        public GameHub(IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }

        private GameState GetStateByUserId(string userId)
        {
            return _gameStateService.GetAllGameStates().Single(x => x.UserId == userId);
        }

        public void AssignPersonToLineServer(int lineId, int personId)
        {
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

        public void InitServerState()
        {
            var gameState = GetStateByUserId(userId);

            if (gameState.Started)
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
                gameState.Started = true;
            }
        }

        public void PreInitServerState()
        {
            var gameState = GetStateByUserId(userId);

            Clients.Caller.PreSetupClientAsync(gameState.Started);
        }

        public void SendDecision(int number)
        {
            var gameState = GetStateByUserId(userId);

            gameState.Player.WaitKeyDayReport = false;
            gameState.Player.WaitForDecision.Choises[number].Apply(gameState);

            gameState.Player.ActiveDecisions = gameState.Player.ActiveDecisions.Skip(1).ToArray();
            if (!gameState.Player.ActiveDecisions.Any())
            {
                gameState.Player.ActiveDecisions = null;
            }

            gameState.Player.WaitForDecision = gameState.Player.ActiveDecisions == null ? null : gameState.Player.ActiveDecisions[0];
        }
    }
}