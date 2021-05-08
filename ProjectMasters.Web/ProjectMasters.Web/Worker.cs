namespace ProjectMasters.Games
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Assets.BL;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    using ProjectMasters.Games.Asserts;
    using ProjectMasters.Web.Hubs;

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHubContext<GameHub, IGame> _gameHub;
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

        private void Initiate()
        {
            GameState._team = new Team();
            GameState._teamFactory = new TeamFactory(GameState._team);
            GameState._teamFactory.Start();
            GameState._project = ProjectUnitFormation.Instance;

            GameState._isLoaded = true;

            GameState.PersonAssigned += GameState_PersonAssigned;
        }

        private void GameState_PersonAssigned(object sender, PersonAssignedEventArgs e)
        {
            _gameHub.Clients.All.AssignPersonAsync(new { PersonId = e.Person.Id, LineId = e.Line.Id });
        }
    }
}