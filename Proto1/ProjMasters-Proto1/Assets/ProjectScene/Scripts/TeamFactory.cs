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
