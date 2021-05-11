namespace ProjectMasters.Games
{
    using System;

    using Assets.BL;

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