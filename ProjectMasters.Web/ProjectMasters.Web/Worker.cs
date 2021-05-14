namespace ProjectMasters.Games
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Asserts;

    using Assets.BL;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    using Web.DTOs;
    using Web.Hubs;
    using Web.Services;

    public class Worker : BackgroundService
    {
        private readonly IHubContext<GameHub, IGame> _gameHub;
        private readonly IGameStateService _gameStateService;
        private readonly ILogger<Worker> _logger;
        private readonly IUserManager _userManager;
        private int _counter;
        private DateTime _currentTime;

        public Worker(IGameStateService gameStateService, IUserManager userManager, ILogger<Worker> logger,
            IHubContext<GameHub, IGame> gameHub)
        {
            _gameStateService = gameStateService;
            _userManager = userManager;
            _logger = logger;
            _gameHub = gameHub;
            _currentTime = DateTime.Now;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var deltaTime = DateTime.Now - _currentTime;
                _currentTime = DateTime.Now;

                var gameStates = _gameStateService.GetAllGameStates();
                foreach (var gameState in gameStates)
                {
                    try
                    {
                        if (!gameState.Started)
                        {
                            await Task.Yield();
                            continue;
                        }

                        if (!gameState.Initialized)
                        {
                            Initiate(gameState);
                        }
                        else
                        {
                            Update(gameState, deltaTime);
                        }
                    }
                    catch (Exception exception)
                    {
                        _logger.LogError(exception, $"Error in worker loop in state of UserId:{gameState.UserId}.");
                    }
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.UtcNow);
                await Task.Delay(1000, stoppingToken);
            }
        }

        private void GameState_DecisionIsStarted(object sender, DecisionEventArgs e)
        {
            var gameState = (GameState)sender;
            var gameHub = GetHubByGameState(gameState);

            gameHub.StartDecision(e.Decision);
            _logger.LogInformation($"{e.Decision.Text} is started");
        }

        private void GameState_EffectIsAdded(object sender, EffectEventArgs e)
        {
            var gameState = (GameState)sender;
            var gameHub = GetHubByGameState(gameState);

            gameHub.AddEffectAsync(e.Effect);
            _logger.LogInformation($"{e.Effect.EffectType} {e.Effect.Id} is added");
        }

        private void GameState_EffectIsRemoved(object sender, EffectEventArgs e)
        {
            var gameState = (GameState)sender;
            var gameHub = GetHubByGameState(gameState);

            gameHub.RemoveEffectAsync(e.Effect);
            _logger.LogInformation($"{e.Effect.EffectType} {e.Effect.Id} is removed");
        }

        private void GameState_LineIsRemoved(object sender, LineEventArgs e)
        {
            var gameState = (GameState)sender;
            var gameHub = GetHubByGameState(gameState);

            gameHub.RemoveLineAsync(new LineDto { Id = e.Line.Id, Persons = e.Line.AssignedPersons });
            _logger.LogInformation($"Decision {e.Line.Id} is removed");
        }

        private void GameState_PersonAssigned(object sender, PersonAssignedEventArgs e)
        {
            var gameState = (GameState)sender;
            var gameHub = GetHubByGameState(gameState);

            gameHub.AssignPersonAsync(new PersonDto(e.Person), new LineDto { Id = e.Line.Id });
            _logger.LogInformation($"Decision {e.Person.Id} assigned to line {e.Line.Id}");
        }

        private void GameState_PersonAttacked(object sender, PersonAttackedEventArgs e)
        {
            var gameState = (GameState)sender;
            var gameHub = GetHubByGameState(gameState);

            gameHub.AttackPersonAsync(new PersonDto(e.Person), new UnitDto(e.Unit));
            _logger.LogInformation($"Decision {e.Person.Id} attacked {e.Unit.GetType()} {e.Unit.Id}");
        }

        private void GameState_PersonIsRested(object sender, PersonEventArgs e)
        {
            var gameState = (GameState)sender;
            var gameHub = GetHubByGameState(gameState);

            gameHub.RestPersonAsync(new PersonDto(e.Person));
            _logger.LogInformation($"{e.Person.Id} is rested");
        }

        private void GameState_PersonIsTired(object sender, PersonEventArgs e)
        {
            var gameState = (GameState)sender;
            var gameHub = GetHubByGameState(gameState);

            gameHub.TirePersonAsync(new PersonDto(e.Person));
            _logger.LogInformation($"{e.Person.Id} is tired");
        }

        private void GameState_SkillIsLearned(object sender, SkillEventArgs e)
        {
            var gameState = (GameState)sender;
            var gameHub = GetHubByGameState(gameState);

            gameHub.SkillIsLearned(e.skill);
            _logger.LogInformation($"{e.skill.Scheme.DisplayTitle} is learned");
        }

        private void GameState_UnitTakenDamage(object sender, UnitTakenDamageEventArgs e)
        {
            var gameState = (GameState)sender;
            var gameHub = GetHubByGameState(gameState);

            gameHub.AnimateUnitDamageAsync(e.UnitDto);
        }

        private string GetConnectionId(GameState gameState)
        {
            var userId = gameState.UserId;

            var connectionId = _userManager.GetConnectionIdByUserId(userId);
            return connectionId;
        }

        private IGame GetHubByGameState(GameState gameState)
        {
            var connectionId = GetConnectionId(gameState);
            var clientHub = _gameHub.Clients.Client(connectionId);
            if (clientHub is null)
            {
                throw new InvalidOperationException("Client with specified id was not found.");
            }

            return clientHub;
        }

        private void Initialized(GameState gameState)
        {
            var personDtos = gameState.Team.Persons.Select(person => new PersonDto(person)
            {
                // Get a line with the person.
                LineId = gameState.Project.Lines.SingleOrDefault(x => x.AssignedPersons.Contains(person))?.Id
            }).ToArray();

            var unitDots = (from line in gameState.Project.Lines from unit in line.Units select new UnitDto(unit))
                .ToList();

            var gameHub = GetHubByGameState(gameState);

            gameHub.SetupClientStateAsync(personDtos, unitDots);
            _logger.LogInformation("Game is started");
        }

        private void Initiate(GameState gameState)
        {
            gameState.Team = new Team();
            gameState.TeamFactory = new TeamFactory(gameState.Team);
            gameState.TeamFactory.Start();
            gameState.Project = ProjectUnitFormation.Instance;
            gameState.Initialized = true;

            gameState.PersonAssigned += GameState_PersonAssigned;
            gameState.PersonAttacked += GameState_PersonAttacked;
            gameState.EffectIsAdded += GameState_EffectIsAdded;
            gameState.EffectIsRemoved += GameState_EffectIsRemoved;
            gameState.PersonIsTired += GameState_PersonIsTired;
            gameState.PersonIsRested += GameState_PersonIsRested;
            gameState.LineIsRemoved += GameState_LineIsRemoved;
            gameState.Project.Added += Project_UnitAdded;
            gameState.Project.Removed += Project_UnitRemoved;
            gameState.DecisionIsStarted += GameState_DecisionIsStarted;
            gameState.UnitTakenDamage += GameState_UnitTakenDamage;
            gameState.SkillIsLearned += GameState_SkillIsLearned;
            Initialized(gameState);
        }

        private void Project_UnitAdded(object sender, UnitEventArgs e)
        {
            var gameState = _gameStateService.GetAllGameStates().Single(x => x.Project == sender);
            var connectionId = _userManager.GetConnectionIdByUserId(gameState.UserId);

            _gameHub.Clients.Client(connectionId).CreateUnitAsync(new UnitDto(e.Unit));
            _logger.LogInformation($"{e.Unit.Type} {e.Unit.Id} is created");
        }

        private void Project_UnitRemoved(object sender, UnitEventArgs e)
        {
            var gameState = _gameStateService.GetAllGameStates().Single(x => x.Project == sender);
            var connectionId = _userManager.GetConnectionIdByUserId(gameState.UserId);

            _gameHub.Clients.All.KillUnitAsync(new UnitDto(e.Unit));
            _logger.LogInformation($"{e.Unit.Type} {e.Unit.Id} is dead");
        }

        private void Update(GameState gameState, TimeSpan deltaTime)
        {
            var time = (float)deltaTime.TotalSeconds;
            gameState.TeamFactory.Update(time, gameState);
            if (_counter == 10)
            {
                var connectionId = _userManager.GetConnectionIdByUserId(gameState.UserId);
                _gameHub.Clients.Client(connectionId).SetStatusAsync(new PlayerDto(gameState));
                _counter = 0;
            }

            _counter++;
        }
    }
}