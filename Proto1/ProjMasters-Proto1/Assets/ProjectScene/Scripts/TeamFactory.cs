using System.Collections.Generic;
using System.Linq;

using Assets.BL;

using UnityEngine;

public class TeamFactory : MonoBehaviour
{
    public PersonViewModel PersonViewModelPrefab;

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
    }

    public void Update()
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
                        line.AssignedPersons = new[] { firstFreePerson };
                        assignedPersons = line.AssignedPersons;
                    }
                }

                foreach (var assignedPerson in assignedPersons)
                {
                    assignedPerson.Update(line.Units.First(), Time.deltaTime);
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
            new Person{Name = "Ivan Ivanov", Skills = new[]{
                new Skill{ Level = 8, Scheme = SkillSchemeCatalog.SkillSchemes[0] }
            },
            Traits = new []{ TraitType.FastLearning } },
            new Person{Name = "Sidre Patron [The Soul]", Skills = new[]{
                new Skill{ Level = 4, Scheme = SkillSchemeCatalog.SkillSchemes[0] },
                new Skill{ Level = 4, Scheme = SkillSchemeCatalog.SkillSchemes[1] }
            },
            Traits = new[]{ TraitType.CarefullDevelopment, TraitType.Apologet } },
            new Person{Name = "John Smith", Skills = new[]{
                new Skill{ Level = 9, Scheme = SkillSchemeCatalog.SkillSchemes[1] }
            },
            Traits = new[]{ TraitType.RapidDevelopment }},
        };
    }
}
