using System.Collections.Generic;

namespace Assets.BL
{
    public sealed class ProjectLine
    {
        public ProjectLine()
        {
            AssignedPersons = new List<Person>();
            Units = new List<ProjectUnitBase>();
        }

        public List<Person> AssignedPersons { get; set; }

        public List<ProjectUnitBase> Units { get; set; }
        public int Id { get; internal set; }
    }
}
