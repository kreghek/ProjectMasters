using System;
using System.Collections.Generic;
using System.Linq;

using Assets.BL;

using ProjectMasters.Games;
using ProjectMasters.Games.Asserts;

public class TeamFactory
{
    private readonly Random _random = new Random(DateTime.Now.Millisecond);
    private readonly Team _team;

    public TeamFactory(Team team)
    {
        _team = team;
    }

    public void Start()
    {
        if (!(_team.Persons is null))
        {
            return;
        }

        var persons = CreateStartTeam();

        // Build in skills are learnt at start.
        foreach (var skill in persons.SelectMany(x => x.Skills))
        {
            skill.IsLearnt = true;
        }

        _team.Persons = persons.ToArray();
    }

    public void Update(float deltaTime)
    {
        if (Player.WaitForDecision != null || Player.WaitTutorial || Player.WaitKeyDayReport)
        {
            GameState.StartDecision(Player.WaitForDecision);
            return;
        }

        UpdateProjectLineSolving(deltaTime);

        UpdateProjectTime(deltaTime);
    }

    private static IEnumerable<Person> CreateStartTeam()
    {
        return new[]
        {
            new Person
            {
                Id = 1,
                Name = "Ivan Ivanov",
                Traits = new[] { TraitType.FastLearning },
                Skills = new[]
                {
                    new Skill
                    {
                        Scheme = SkillCatalog.AllSchemes.Single(x =>
                            x.Sid == SkillCatalog.Sids.JavaScriptFoundations + "-1")
                    },
                    new Skill
                    {
                        Scheme = SkillCatalog.AllSchemes.Single(x =>
                            x.Sid == SkillCatalog.Sids.JavaScriptFoundations + "-2")
                    },
                    new Skill
                    {
                        Scheme = SkillCatalog.AllSchemes.Single(x =>
                            x.Sid == SkillCatalog.Sids.JavaScriptReactiveProgramming + "-1")
                    },
                    new Skill
                    {
                        Scheme = SkillCatalog.AllSchemes.Single(x =>
                            x.Sid == SkillCatalog.Sids.JavaScriptReactiveProgramming + "-2")
                    },
                    new Skill
                    {
                        Scheme = SkillCatalog.AllSchemes.Single(x =>
                            x.Sid == SkillCatalog.Sids.AngularFoundations + "-1")
                    }
                },

                EyesIndex = 1,
                HairIndex = 1,
                MouthIndex = 1
            },

            new Person
            {
                Id = 2,
                Name = "Sidre Patron [The Soul]",
                Traits = new[] { TraitType.CarefullDevelopment, TraitType.Apologet },
                Skills = new[]
                {
                    new Skill
                    {
                        Scheme = SkillCatalog.AllSchemes.Single(
                            x => x.Sid == SkillCatalog.Sids.CSharpFoundations + "-1")
                    },
                    new Skill
                    {
                        Scheme = SkillCatalog.AllSchemes.Single(x =>
                            x.Sid == SkillCatalog.Sids.DotnetAsyncProgramming + "-1")
                    },
                    new Skill
                    {
                        Scheme = SkillCatalog.AllSchemes.Single(x =>
                            x.Sid == SkillCatalog.Sids.DotnetAsyncProgramming + "-2")
                    },
                    new Skill
                    {
                        Scheme = SkillCatalog.AllSchemes.Single(x =>
                            x.Sid == SkillCatalog.Sids.AspNetCoreFoundations + "-1")
                    },

                    new Skill
                    {
                        Scheme = SkillCatalog.AllSchemes.Single(x =>
                            x.Sid == SkillCatalog.Sids.JavaScriptFoundations + "-1")
                    },
                    new Skill
                    {
                        Scheme = SkillCatalog.AllSchemes.Single(x =>
                            x.Sid == SkillCatalog.Sids.JavaScriptReactiveProgramming + "-1")
                    },
                    new Skill
                    {
                        Scheme = SkillCatalog.AllSchemes.Single(x =>
                            x.Sid == SkillCatalog.Sids.AngularFoundations + "-1")
                    }
                },

                EyesIndex = 2,
                HairIndex = 2,
                MouthIndex = 2
            },

            new Person
            {
                Id = 3,
                Name = "John Smith",
                Traits = new[] { TraitType.RapidDevelopment },
                Skills = new[]
                {
                    new Skill
                    {
                        Scheme = SkillCatalog.AllSchemes.Single(
                            x => x.Sid == SkillCatalog.Sids.CSharpFoundations + "-1")
                    },
                    new Skill
                    {
                        Scheme = SkillCatalog.AllSchemes.Single(x =>
                            x.Sid == SkillCatalog.Sids.DotnetAsyncProgramming + "-1")
                    },
                    new Skill
                    {
                        Scheme = SkillCatalog.AllSchemes.Single(x =>
                            x.Sid == SkillCatalog.Sids.DotnetAsyncProgramming + "-2")
                    },
                    new Skill
                    {
                        Scheme = SkillCatalog.AllSchemes.Single(x =>
                            x.Sid == SkillCatalog.Sids.AspNetCoreFoundations + "-1")
                    },
                    new Skill
                    {
                        Scheme = SkillCatalog.AllSchemes.Single(x =>
                            x.Sid == SkillCatalog.Sids.AspNetCoreFoundations + "-2")
                    }
                },

                EyesIndex = 3,
                HairIndex = 3,
                MouthIndex = 3
            }
        };
    }

    private void HandleDecision()
    {
        var decisionCount = _random.Next(2, 5);
        Player.ActiveDecisions = new Decision[decisionCount];
        for (var i = 0; i < decisionCount; i++)
        {
            var rolledDecisionIndex = _random.Next(0, DecisionCatalog.Decisions.Length);
            Player.ActiveDecisions[i] = DecisionCatalog.Decisions[rolledDecisionIndex];
        }

        Player.WaitForDecision = Player.ActiveDecisions[0];
    }

    private void UpdateDayly()
    {
        foreach (var person in _team.Persons)
        {
            person.DaylyUpdate();
        }

        HandleDecision();

        Player.WaitKeyDayReport = true;
    }

    private void UpdateProjectLineSolving(float deltaTime)
    {
        foreach (var line in ProjectUnitFormation.Instance.Lines.ToArray())
        {
            if (line.Units.Any())
            {
                var assignedPersons = line.AssignedPersons;
                if (!assignedPersons.Any())
                {
                    var freePersons = _team.Persons
                        .Except(ProjectUnitFormation.Instance.Lines.SelectMany(x => x.AssignedPersons).Distinct())
                        .ToArray();
                    var firstFreePerson = freePersons.FirstOrDefault();

                    if (firstFreePerson != null)
                    {
                        line.AssignedPersons.Add(firstFreePerson);

                        GameState.AssignPerson(line, firstFreePerson);

                        assignedPersons = line.AssignedPersons;
                    }
                }

                foreach (var assignedPerson in assignedPersons)
                {
                    assignedPerson.Update(line.Units, line.AssignedPersons, deltaTime);
                }
            }
            else
            {
                ProjectUnitFormation.Instance.Lines.Remove(line);
            }
        }
    }

    private void UpdateProjectTime(float deltaTime)
    {
        Player.DayCounter -= deltaTime;

        if (Player.DayCounter > 0)
        {
            return;
        }

        Player.DayCounter = Player.DAY_COUNTER_BASE;
        Player.DayNumber++;

        UpdateDayly();

        // payment
        if (Player.Money > 0)
        {
            foreach (var person in _team.Persons)
            {
                Player.Money -= person.DaylyPayment;
            }
        }
        else
        {
            Player.FailureCount++;
        }
    }
}