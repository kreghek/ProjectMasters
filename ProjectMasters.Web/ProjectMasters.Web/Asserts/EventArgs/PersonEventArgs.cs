namespace ProjectMasters.Games
{
    using System;

    using Assets.BL;

    public class PersonEventArgs : EventArgs
    {
        public PersonEventArgs(Person person)
        {
            Person = person ?? throw new ArgumentNullException(nameof(person));
        }

        public Person Person { get; }
    }
}