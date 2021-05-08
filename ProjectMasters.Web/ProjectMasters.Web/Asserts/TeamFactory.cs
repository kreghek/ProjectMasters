using System;
using System.Collections.Generic;
using System.Linq;

using Assets.BL;

using ProjectMasters.Games.Asserts;

public class TeamFactory
{
    private Team Team;
    private Random Random = new Random(DateTime.Now.Millisecond);
    public TeamFactory(Team team)
    {
        Team = team;
    }

    public void Start()
    {
        if (!(Team.Persons is null))
            return;

        var persons = CreateStartTeam();

        // Build in skills are learnt at start.
        foreach (var skill in persons.SelectMany(x => x.Skills))
        {
            skill.IsLearnt = true;
        }

        Team.Persons = persons.ToArray();
    }

    public void Update(float deltaTime)
    {
        if (Player.WaitForDecision != null || Player.WaitTutorial || Player.WaitKeyDayReport)
        {
            return;
        }

        UpdateProjectLineSolving(deltaTime);

        UpdateProjectTime(deltaTime);

    }

    private void HandleDecision()
    {
        var decisionCount = Random.Next(2, 5);
        Player.ActiveDecisions = new Decision[decisionCount];
        for (int i = 0; i < decisionCount; i++)
        {
            var rolledDecisionIndex = Random.Next(0, DecisionCatalog.Decisions.Length);
            Player.ActiveDecisions[i] = DecisionCatalog.Decisions[rolledDecisionIndex];
        }

        Player.WaitForDecision = Player.ActiveDecisions[0];
    }



    private void UpdateProjectTime(float deltaTime)
    {
        Player.DayCounter -= deltaTime;

        if (Player.DayCounter > 0)
            return;

        Player.DayCounter = Player.DAY_COUNTER_BASE;
        Player.DayNumber++;

        UpdateDayly();

        // payment
        if (Player.Money > 0)
        {
            foreach (var person in Team.Persons)
            {
                Player.Money -= person.DaylyPayment;
            }
        }
        else
        {
            Player.FailureCount++;
        }
    }

    private void UpdateDayly()
    {
        foreach (var person in Team.Persons)
        {
            person.DaylyUpdate();
        }

        //HandleDecision();
    }

    private void UpdateProjectLineSolving(float deltaTime)
    {
        foreach (var line in ProjectUnitFormation.Instance.Lines.ToArray())
        {
            if (line.Units.Any())
            {
                var assignedPersons = line.AssignedPersons;
                if (assignedPersons.Count() == 0)
                {
                    var freePersons = Team.Persons.Except(ProjectUnitFormation.Instance.Lines.SelectMany(x => x.AssignedPersons).Distinct()).ToArray();
                    var firstFreePerson = freePersons.FirstOrDefault();

                    if (firstFreePerson != null)
                    {
                        line.AssignedPersons.Add(firstFreePerson);
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
}
