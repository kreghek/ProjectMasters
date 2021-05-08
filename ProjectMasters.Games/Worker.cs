namespace ProjectMasters.Games
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Assets.BL;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    using ProjectMasters.Games.Asserts;

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private DateTime _currentTime;
        private bool _isLoaded;
        private ProjectUnitFormation _project;
        private Team _team;
        private TeamFactory _teamFactory;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _currentTime = DateTime.Now;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var deltaTime = DateTime.Now - _currentTime;
                _currentTime = DateTime.Now;

                if (!_isLoaded)
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

            _teamFactory.Update(time);
        }

        private void Initiate()
        {
            _team = new Team();
            _teamFactory = new TeamFactory(_team);
            _teamFactory.Start();
            _project = ProjectUnitFormation.Instance;

            _isLoaded = true;
        }
    }
}