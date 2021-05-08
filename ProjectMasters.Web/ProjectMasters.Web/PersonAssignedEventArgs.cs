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

    public class UnitEventArgs : EventArgs
    {
        public UnitEventArgs(ProjectUnitBase unit)
        {
            Unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        public ProjectUnitBase Unit { get; }
    }

    public class EffectEventArgs : EventArgs
    {
        public EffectEventArgs(Effect effect)
        {
            Effect = effect ?? throw new ArgumentNullException(nameof(effect));
        }

        public Effect Effect { get; }
    }

    public class PersonEventArgs : EventArgs
    {
        public PersonEventArgs(Person person)
        {
            Person = person ?? throw new ArgumentNullException(nameof(person));
        }

        public Person Person { get; }
    }
    
}