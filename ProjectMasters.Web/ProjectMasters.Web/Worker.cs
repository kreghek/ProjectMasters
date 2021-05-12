namespace ProjectMasters.Games
{
    using System;
    using System.Data.SqlTypes;
    using System.Diagnostics.Tracing;
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

    public class Worker : BackgroundService
    {
        private readonly IHubContext<GameHub, IGame> _gameHub;
        private readonly IGameStateService _gameStateService;
        private readonly ILogger<Worker> _logger;
        private int _counter;
        private DateTime _currentTime;

        public Worker(IGameStateService gameStateService, ILogger<Worker> logger, IHubContext<GameHub, IGame> gameHub)
        {
            _gameStateService = gameStateService;

            _logger = logger;
            _gameHub = gameHub;
            _currentTime = DateTime.Now;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var gameStates = _gameStateService.GetAllGameStates();
                foreach (var gameState in gameStates)
                {
                    if (!gameState.Started)
                    {
                        await Task.Yield();
                        continue;
                    }

                    var deltaTime = DateTime.Now - _currentTime;
                    _currentTime = DateTime.Now;

                    if (!gameState.Initialized)
                    {
                        Initiate(gameState);
                    }
                    else
                    {
                        Update(gameState, deltaTime);
                    }
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.UtcNow);
                await Task.Delay(1000, stoppingToken);
            }
        }

        private void GameState_DecisionIsStarted(object sender, DecisionEventArgs e)
        {
            _gameHub.Clients.All.StartDecision(e.Decision);
            _logger.LogInformation($"{e.Decision.Text} is started");
        }

        private void GameState_EffectIsAdded(object sender, EffectEventArgs e)
        {
            _gameHub.Clients.All.AddEffectAsync(e.Effect);
            _logger.LogInformation($"{e.Effect.EffectType} {e.Effect.Id} is added");
        }

        private void GameState_EffectIsRemoved(object sender, EffectEventArgs e)
        {
            _gameHub.Clients.All.RemoveEffectAsync(e.Effect);
            _logger.LogInformation($"{e.Effect.EffectType} {e.Effect.Id} is removed");
        }

        private void GameState_LineIsRemoved(object sender, LineEventArgs e)
        {
            _gameHub.Clients.All.RemoveLineAsync(new LineDto { Id = e.Line.Id, Persons = e.Line.AssignedPersons });
            _logger.LogInformation($"Decision {e.Line.Id} is removed");
        }

        private void GameState_PersonAssigned(object sender, PersonAssignedEventArgs e)
        {
            _gameHub.Clients.All.AssignPersonAsync(new PersonDto(e.Person), new LineDto { Id = e.Line.Id });
            _logger.LogInformation($"Decision {e.Person.Id} assigned to line {e.Line.Id}");
        }

        private void GameState_PersonAttacked(object sender, PersonAttackedEventArgs e)
        {
            _gameHub.Clients.All.AttackPersonAsync(new PersonDto(e.Person), new UnitDto(e.Unit));
            _logger.LogInformation($"Decision {e.Person.Id} attacked {e.Unit.GetType()} {e.Unit.Id}");
        }

        private void GameState_PersonIsRested(object sender, PersonEventArgs e)
        {
            _gameHub.Clients.All.RestPersonAsync(new PersonDto(e.Person));
            _logger.LogInformation($"{e.Person.Id} is rested");
        }

        private void GameState_PersonIsTired(object sender, PersonEventArgs e)
        {
            _gameHub.Clients.All.TirePersonAsync(new PersonDto(e.Person));
            _logger.LogInformation($"{e.Person.Id} is tired");
        }

        private void GameState_SkillIsLearned(object sender, SkillEventArgs e)
        {
            _gameHub.Clients.All.SkillIsLearned(e.skill);
            _logger.LogInformation($"{e.skill.Scheme.DisplayTitle} is learned");
        }

        private void GameState_UnitTakenDamage(object sender, UnitTakenDamageEventArgs e)
        {
            _gameHub.Clients.All.AnimateUnitDamageAsync(e.UnitDto);
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

            _gameHub.Clients.All.SetupClientStateAsync(personDtos, unitDots);
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
            _gameHub.Clients.All.CreateUnitAsync(new UnitDto(e.Unit));
            _logger.LogInformation($"{e.Unit.Type} {e.Unit.Id} is created");
        }

        private void Project_UnitRemoved(object sender, UnitEventArgs e)
        {
            _gameHub.Clients.All.KillUnitAsync(new UnitDto(e.Unit));
            _logger.LogInformation($"{e.Unit.Type} {e.Unit.Id} is dead");
        }

        private void Update(GameState gameState, TimeSpan deltaTime)
        {
            var time = (float)deltaTime.TotalSeconds;
            gameState.TeamFactory.Update(time, gameState);
            if (_counter == 10)
            {
                _gameHub.Clients.All.SetStatusAsync(new PlayerDto(gameState));
                _counter = 0;
            }

            _counter++;
        }
    }
}