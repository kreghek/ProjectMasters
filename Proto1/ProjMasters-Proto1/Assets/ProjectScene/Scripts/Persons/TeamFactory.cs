using System.Collections.Generic;
using System.Linq;

using Assets.BL;

using UnityEngine;

public class TeamFactory : MonoBehaviour
{
    public PersonViewModel PersonViewModelPrefab;
    public PersonsPanelHandler PersonsPanelHandler;

    void Start()
    {
        if (Team.Persons == null)
        {
            var persons = CreateStartTeam();
            foreach (var person in persons)
            {
                var personViewModel = Instantiate(PersonViewModelPrefab);
                personViewModel.Person = person;
            }

            Team.Persons = persons.ToArray();
        }

        PersonsPanelHandler.Init();
    }

    public void Update()
    {
        var deltaTime = Time.deltaTime;

        //if (Player.WaitForDecision != null || Player.WaitTutorial)
        //{
        //    return;
        //}

        UpdateProjectLineSolving(deltaTime);

        UpdateProjectTime(deltaTime);

        HandleDecision(deltaTime);

        ProcessGameOver();
    }

    private void HandleDecision(float deltaTime)
    {
        if (Player.DecisionCounter <= 0)
        {
            if (Random.Range(1, 100) < 25)
            {
                var decisionIndex = Random.Range(0, DecisionCatalog.Decisions.Length);
                Player.WaitForDecision = DecisionCatalog.Decisions[decisionIndex];
                Player.DecisionCounter = Player.DECISION_COUNTER_BASE;
            }
        }
        else
        {
            Player.DecisionCounter -= deltaTime;
        }
    }

    private static void ProcessGameOver()
    {
        if (Player.FailureCount > 3)
        {
            // Game over
        }
    }

    private static void UpdateProjectTime(float deltaTime)
    {
        Player.DayCounter -= deltaTime;
        if (Player.DayCounter <= 0)
        {
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
    }

    private static void UpdateDayly()
    {
        foreach (var person in Team.Persons)
        {
            person.DaylyUpdate();
        }
    }

    private static void UpdateProjectLineSolving(float deltaTime)
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
                    assignedPerson.Update(line.Units.First(), deltaTime);
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
                Skills = new[] { new Skill{ Level = 8, Scheme = SkillSchemeCatalog.SkillSchemes[0] } },
                Traits = new []{ TraitType.FastLearning },
                EyeIndex = 0,
                FaceDecorIndex = 1,
            },

            new Person
            {
                Name = "Sidre Patron [The Soul]",
                Skills = new[]
                { 
                    new Skill{ Level = 4, Scheme = SkillSchemeCatalog.SkillSchemes[0] },
                    new Skill{ Level = 4, Scheme = SkillSchemeCatalog.SkillSchemes[1] }
                },
                Traits = new[]{ TraitType.CarefullDevelopment, TraitType.Apologet },
                EyeIndex = 1,
                FaceDecorIndex = 2
            },

            new Person
            {
                Name = "John Smith",
                Skills = new[]
                {
                    new Skill{ Level = 9, Scheme = SkillSchemeCatalog.SkillSchemes[1] }
                },
                Traits = new[]{ TraitType.RapidDevelopment },
                EyeIndex = 2,
                FaceDecorIndex = 0
            },
        };
    }
}
