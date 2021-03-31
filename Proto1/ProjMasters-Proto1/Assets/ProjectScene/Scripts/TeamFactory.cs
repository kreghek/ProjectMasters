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
            var personIndex = 0;
            foreach (var person in persons)
            {
                var personViewModel = Instantiate(PersonViewModelPrefab);
                personViewModel.Person = person;
                personIndex++;
                personViewModel.gameObject.transform.localPosition = new Vector3(0, personIndex * 1, 0);
            }

            Team.Persons = persons.ToArray();
        }
    }

    public void Update()
    {
        PlanPersonsToUnits(Team.Persons);

        foreach (var person in Team.Persons)
        {
            person.Update(Time.deltaTime);
        }
    }

    private static void PlanPersonsToUnits(Person[] persons)
    {
        foreach (var person in persons)
        {
            if (person.Assigned != null)
            {
                continue;
            }

            var unit = FindFreeUnit();
            if (unit != null)
            {
                person.Assigned = unit;
            }
        }
    }

    private static ProjectUnitBase FindFreeUnit()
    {
        var matrix = ProjectUnitFormation.Instance.Matrix;

        for (var j = 0; j < matrix.GetLength(1); j++)
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                var unit = matrix[i, j];

                if (unit != null)
                {
                    var assignedPersons = Team.Persons.Where(x => x.Assigned == unit);
                    if (!assignedPersons.Any())
                    {
                        var leftX = i - 1;
                        if (leftX < 0)
                        {
                            return unit;
                        }
                        else
                        {
                            var blockerUnit = matrix[leftX, j];
                            if (blockerUnit is null)
                            {
                                return unit;
                            }
                        }
                    }
                }
            }
        }

        return null;
    }

    private IEnumerable<Person> CreateStartTeam()
    {
        return new[] {
            new Person{Name = "Ivan Ivanov", Speed = 1, Skills = new[]{
                new Skill{ Level = 8, Scheme = SkillSchemeCatalog.SkillSchemes[0] }
            } },
            new Person{Name = "Sidre Patre", Speed = 2, Skills = new[]{
                new Skill{ Level = 4, Scheme = SkillSchemeCatalog.SkillSchemes[0] },
                new Skill{ Level = 4, Scheme = SkillSchemeCatalog.SkillSchemes[1] }
            } },
            new Person{Name = "John Smith", Speed = 0.75f, Skills = new[]{
                new Skill{ Level = 9, Scheme = SkillSchemeCatalog.SkillSchemes[1] }
            } },
        };
    }
}
