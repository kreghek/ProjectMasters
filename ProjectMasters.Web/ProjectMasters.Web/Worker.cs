namespace ProjectMasters.Games
{
    using System;
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
            //foreach (var line in ProjectUnitFormation.Instance.Lines)
            //{
            //    _logger.LogError(line.Units.Count.ToString());
            //}
        }

        private void Initiate()
        {
            GameState._team = new Team();
            GameState._teamFactory = new TeamFactory(GameState._team);
            GameState._teamFactory.Start();
            GameState._project = ProjectUnitFormation.Instance;

            GameState._isLoaded = true;
        }
    }
}