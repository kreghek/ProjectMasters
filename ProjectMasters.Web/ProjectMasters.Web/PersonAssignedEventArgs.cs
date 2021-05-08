namespace ProjectMasters.Games
{
    using System;

    using Assets.BL;

    public class PersonAssignedEventArgs : EventArgs
    {
        public PersonAssignedEventArgs(ProjectLine line, Person person)
        {
            Person = person ?? throw new ArgumentNullException(nameof(person));
            Line = line ?? throw new ArgumentNullException(nameof(line));
        }

        public Person Person { get; }
        public ProjectLine Line { get; }
    }

    public class PersonAttackedEventArgs : EventArgs
    {
        public PersonAttackedEventArgs(ProjectUnitBase unit, Person person)
        {
            Person = person ?? throw new ArgumentNullException(nameof(person));
            Unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        public Person Person { get; }
        public ProjectUnitBase Unit { get; }
    }
}