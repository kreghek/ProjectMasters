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

        public static event EventHandler<PersonAssignedEventArgs> PersonAssigned;
    }
}