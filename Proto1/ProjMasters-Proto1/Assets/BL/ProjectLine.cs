using System;
using System.Collections.Generic;

namespace Assets.BL
{
    public sealed class ProjectLine
    {
        public ProjectLine()
        {
            AssignedPersons = Array.Empty<Person>();
            Units = new List<ProjectUnitBase>();
        }

        public Person[] AssignedPersons { get; set; }

        public List<ProjectUnitBase> Units { get; set; }
    }
}
