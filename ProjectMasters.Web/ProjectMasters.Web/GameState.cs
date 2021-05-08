namespace ProjectMasters.Games
{
    using System;

    using Assets.BL;

    using ProjectMasters.Games.Asserts;

    public static class GameState
    {
        public static bool _isLoaded;
        public static ProjectUnitFormation _project;
        public static Team _team;
        public static TeamFactory _teamFactory;

        internal static void AssignPerson(ProjectLine line, Person person)
        {
            PersonAssigned?.Invoke(null, new PersonAssignedEventArgs(line, person));
        }

        internal static void AttackPerson(ProjectUnitBase unit, Person person)
        {
            PersonAttacked?.Invoke(null, new PersonAttackedEventArgs(unit, person));
        }

        internal static void KillUnit(ProjectUnitBase unit)
        {
            UnitIsDead?.Invoke(null, new UnitIsDeadEventArgs(unit));
        }

        public static event EventHandler<PersonAssignedEventArgs> PersonAssigned;
        public static event EventHandler<PersonAttackedEventArgs> PersonAttacked;
        public static event EventHandler<UnitIsDeadEventArgs> UnitIsDead;
        public static event EventHandler<UnitIsCreatedEventArgs> UnitIsCreated;


        public static void CreateUnit(ProjectUnitBase unit)
        {
            UnitIsCreated?.Invoke(null, new UnitIsCreatedEventArgs(unit));
        }
    }
}