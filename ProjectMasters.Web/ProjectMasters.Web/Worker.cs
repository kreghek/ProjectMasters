namespace ProjectMasters.Games
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Assets.BL;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    using ProjectMasters.Games.Asserts;
    using ProjectMasters.Web.DTOs;
    using ProjectMasters.Web.Hubs;

    public class Worker : BackgroundService
    {
        private readonly IHubContext<GameHub, IGame> _gameHub;
        private readonly ILogger<Worker> _logger;
        private DateTime _currentTime;

        public Worker(ILogger<Worker> logger, IHubContext<GameHub, IGame> gameHub)
        {
            _logger = logger;
            _gameHub = gameHub;
            _currentTime = DateTime.Now;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (!GameState.Started)
                    continue;

                var deltaTime = DateTime.Now - _currentTime;
                _currentTime = DateTime.Now;

                if (!GameState._isLoaded)
                    Initiate();
                else
                    DoLogic(deltaTime);

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        private void DoLogic(TimeSpan deltaTime)
        {
            var time = (float)deltaTime.TotalSeconds;

            GameState._teamFactory.Update(time);
        }

        private void GameState_EffectIsAdded(object sender, EffectEventArgs e)
        {
            _gameHub.Clients.All.AddEffectAsync(e.Effect);
            _logger.LogError($"{e.Effect.EffectType} {e.Effect.Id} is added");
        }

        private void GameState_EffectIsRemoved(object sender, EffectEventArgs e)
        {
            _gameHub.Clients.All.RemoveEffectAsync(e.Effect);
            _logger.LogError($"{e.Effect.EffectType} {e.Effect.Id} is removed");
        }

        private void GameState_PersonAssigned(object sender, PersonAssignedEventArgs e)
        {
            _gameHub.Clients.All.AssignPersonAsync(new PersonDto(e.Person), new LineDto { Id = e.Line.Id });
            _logger.LogInformation($"Line {e.Person.Id} assigned to line {e.Line.Id}");
        }

        private void GameState_PersonAttacked(object sender, PersonAttackedEventArgs e)
        {
            _gameHub.Clients.All.AttackPersonAsync(new PersonDto(e.Person), new UnitDto(e.Unit));
            _logger.LogInformation($"Line {e.Person.Id} attacked {e.Unit.GetType()} {e.Unit.Id}");
        }

        private void GameState_PersonIsRested(object sender, PersonEventArgs e)
        {
            _gameHub.Clients.All.RestPersonAsync(new PersonDto(e.Person));
            _logger.LogError($"{e.Person.Id} is rested");
        }

        private void GameState_PersonIsTired(object sender, PersonEventArgs e)
        {
            _gameHub.Clients.All.TirePersonAsync(new PersonDto(e.Person));
            _logger.LogError($"{e.Person.Id} is tired");
        }

        private void GameState_UnitIsCreated(object sender, UnitEventArgs e)
        {
            _gameHub.Clients.All.CreateUnitAsync(new UnitDto(e.Unit));
            _logger.LogInformation($"{e.Unit.Type} {e.Unit.Id} is created");
        }

        private void GameState_UnitIsDead(object sender, UnitEventArgs e)
        {
            _gameHub.Clients.All.KillUnitAsync(new UnitDto(e.Unit));
            _logger.LogInformation($"{e.Unit.Type} {e.Unit.Id} is dead");
        }

        private void GameState_LineIsRemoved(object sender, LineEventArgs e)
        {
            _gameHub.Clients.All.RemoveLineAsync(new LineDto { Id = e.Line.Id });
            _logger.LogError($"Line {e.Line.Id} is removed");
        }

        private void Initiate()
        {
            GameState._team = new Team();
            GameState._teamFactory = new TeamFactory(GameState._team);
            GameState._teamFactory.Start();
            GameState._project = ProjectUnitFormation.Instance;

            GameState._isLoaded = true;

            GameState.PersonAssigned += GameState_PersonAssigned;
            GameState.PersonAttacked += GameState_PersonAttacked;
            GameState.UnitIsDead += GameState_UnitIsDead;
            GameState.UnitIsCreated += GameState_UnitIsCreated;
            GameState.EffectIsAdded += GameState_EffectIsAdded;
            GameState.EffectIsRemoved += GameState_EffectIsRemoved;
            GameState.PersonIsTired += GameState_PersonIsTired;
            GameState.PersonIsRested += GameState_PersonIsRested;
            GameState.LineIsRemoved += GameState_LineIsRemoved;

        }
    }
}