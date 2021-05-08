using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ProjectMasters.Games
{
    using Assets.BL;

    using ProjectMasters.Games.Asserts;

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private bool IsLoaded;
        private Project project;
        private Team team;
        private DateTime currentTime;
        private object Lines;

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
                    Initiate();
                }
                else
                {
                    DoLogic(deltaTime, project);
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        private void Initiate()
        {
            FillLines1();

            team = new Team()
            {
            };
            IsLoaded = true;
        }

        private static IEnumerable<Person> CreateStartTeam()
        {
            return new[] {
            new Person
            {
                Name = "Ivan Ivanov",
                Traits = new []{ TraitType.FastLearning },
                Skills = new []{
                    new Skill { Scheme = SkillCatalog.AllSchemes.Single(x=>x.Sid == SkillCatalog.Sids.JavaScriptFoundations + "-1") },
                    new Skill { Scheme = SkillCatalog.AllSchemes.Single(x=>x.Sid == SkillCatalog.Sids.JavaScriptFoundations + "-2") },
                    new Skill { Scheme = SkillCatalog.AllSchemes.Single(x=>x.Sid == SkillCatalog.Sids.JavaScriptReactiveProgramming + "-1") },
                    new Skill { Scheme = SkillCatalog.AllSchemes.Single(x=>x.Sid == SkillCatalog.Sids.JavaScriptReactiveProgramming + "-2") },
                    new Skill { Scheme = SkillCatalog.AllSchemes.Single(x=>x.Sid == SkillCatalog.Sids.AngularFoundations + "-1") },
                },

                EyeIndex = 0,
                FaceDecorIndex = 1,
            },

            new Person
            {
                Name = "Sidre Patron [The Soul]",
                Traits = new[]{ TraitType.CarefullDevelopment, TraitType.Apologet },
                Skills = new []{
                    new Skill { Scheme = SkillCatalog.AllSchemes.Single(x=>x.Sid == SkillCatalog.Sids.CSharpFoundations + "-1") },
                    new Skill { Scheme = SkillCatalog.AllSchemes.Single(x=>x.Sid == SkillCatalog.Sids.DotnetAsyncProgramming + "-1") },
                    new Skill { Scheme = SkillCatalog.AllSchemes.Single(x=>x.Sid == SkillCatalog.Sids.DotnetAsyncProgramming + "-2") },
                    new Skill { Scheme = SkillCatalog.AllSchemes.Single(x=>x.Sid == SkillCatalog.Sids.AspNetCoreFoundations + "-1") },

                    new Skill { Scheme = SkillCatalog.AllSchemes.Single(x=>x.Sid == SkillCatalog.Sids.JavaScriptFoundations + "-1") },
                    new Skill { Scheme = SkillCatalog.AllSchemes.Single(x=>x.Sid == SkillCatalog.Sids.JavaScriptReactiveProgramming + "-1") },
                    new Skill { Scheme = SkillCatalog.AllSchemes.Single(x=>x.Sid == SkillCatalog.Sids.AngularFoundations + "-1") },
                },

                EyeIndex = 1,
                FaceDecorIndex = 2
            },

            new Person
            {
                Name = "John Smith",
                Traits = new[]{ TraitType.RapidDevelopment },
                Skills = new []{
                    new Skill {  Scheme = SkillCatalog.AllSchemes.Single(x=>x.Sid == SkillCatalog.Sids.CSharpFoundations + "-1") },
                    new Skill {  Scheme = SkillCatalog.AllSchemes.Single(x=>x.Sid == SkillCatalog.Sids.DotnetAsyncProgramming + "-1") },
                    new Skill {  Scheme = SkillCatalog.AllSchemes.Single(x=>x.Sid == SkillCatalog.Sids.DotnetAsyncProgramming + "-2") },
                    new Skill {  Scheme = SkillCatalog.AllSchemes.Single(x=>x.Sid == SkillCatalog.Sids.AspNetCoreFoundations + "-1") },
                    new Skill {  Scheme = SkillCatalog.AllSchemes.Single(x=>x.Sid == SkillCatalog.Sids.AspNetCoreFoundations + "-2") },
                },
                EyeIndex = 2,
                FaceDecorIndex = 0
            },
        };
        }

        private void CreateTeam()
        {
            if (team.Persons is null)
            {
                var persons = CreateStartTeam();

                // Build in skills are learnt at start.
                foreach (var skill in persons.SelectMany(x => x.Skills))
                {
                    skill.IsLearnt = true;
                }

                team.Persons = persons.ToArray();
            }

            //foreach (var person in Team.Persons)
            //{
            //    var personViewModel = Instantiate(PersonViewModelPrefab);
            //    personViewModel.Person = person;
            //}

            //PersonsPanelHandler.Init();
        }

        private void FillLines1()
        {
            project = new Project()
            {
                Lines = new List<ProjectLine>()
            };
            project.Lines.Add(new ProjectLine
            {
                Units = new List<ProjectUnitBase>
                {
                    new FeatureUnit
                    {
                        Cost = 2,
                        LineIndex = 0,
                        QueueIndex = 0
                    }
                }
            });
            project.Lines.Add(new ProjectLine
            {
                Units = new List<ProjectUnitBase>
                {
                    new FeatureUnit
                    {
                        Cost = 1,
                        LineIndex = 1,
                        QueueIndex = 0
                    }
                }
            });
            project.Lines.Add(new ProjectLine
            {
                Units = new List<ProjectUnitBase>
                {
                    new FeatureUnit
                    {
                        Cost = 0.8f,
                        LineIndex = 2,
                        QueueIndex = 0
                    }
                }
            });
        }

        private void DoLogic(TimeSpan deltaTime, Project project)
        {
            team.Update(deltaTime, project);
        }


    }
}
