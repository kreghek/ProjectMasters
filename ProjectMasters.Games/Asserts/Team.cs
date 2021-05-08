namespace ProjectMasters.Games.Asserts
{
    using System;
    using System.Linq;

    using Assets.BL;

    public class Team
    {
        public Person[] Persons;

        public void Update(TimeSpan deltaTime, Project project)
        {
            foreach (var person in Persons)
            {
                var line = project.Lines.FirstOrDefault(p => p.AssignedPersons.Contains(person));
                if (line != null)
                    person.Update(project.Lines.SelectMany(u => u.Units).ToList(), line.AssignedPersons,
                        (float)deltaTime.TotalSeconds);
            }
        }
    }
}