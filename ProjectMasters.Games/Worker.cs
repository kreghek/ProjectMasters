using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ProjectMasters.Games
{
    using ProjectMasters.Games.Asserts;

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private bool IsLoaded;
        private Project project;
        private Team team;
        private DateTime currentTime;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            currentTime = DateTime.Now;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                var deltaTime = DateTime.Now - currentTime;
                currentTime = DateTime.Now;

                if (!IsLoaded)
                {
                    project = new Project()
                    {

                    };
                    team = new Team()
                    {

                    };
                    IsLoaded = true;
                }
                else
                {
                    DoLogic(deltaTime);

                }


                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        private void DoLogic(TimeSpan deltaTime, Project project)
        {
            team.Update(deltaTime, project);
        }
    }
}
