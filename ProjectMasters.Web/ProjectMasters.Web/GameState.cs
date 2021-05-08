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

        public static bool Started { get; internal set; }
        public static void AddEffect(Effect effect)
        {
            EffectIsAdded?.Invoke(null, new EffectEventArgs(effect));
        }

        public static event EventHandler<EffectEventArgs> EffectIsAdded;
        public static event EventHandler<EffectEventArgs> EffectIsRemoved;

        public static event EventHandler<PersonAssignedEventArgs> PersonAssigned;
        public static event EventHandler<PersonAttackedEventArgs> PersonAttacked;
        public static event EventHandler<PersonEventArgs> PersonIsRested;
        public static event EventHandler<PersonEventArgs> PersonIsTired;
        public static event EventHandler<LineEventArgs> LineIsRemoved;
        public static event EventHandler<DecisionEventArgs> DecisionIsStarted;


        public static void RemoveEffect(Effect effect)
        {
            EffectIsRemoved?.Invoke(null, new EffectEventArgs(effect));
        }

        public static void RestPerson(Person person)
        {
            PersonIsRested?.Invoke(null, new PersonEventArgs(person));
        }

        public static void TirePerson(Person person)
        {
            PersonIsTired?.Invoke(null, new PersonEventArgs(person));
        }

        internal static void AssignPerson(ProjectLine line, Person person)
        {
            PersonAssigned?.Invoke(null, new PersonAssignedEventArgs(line, person));
        }

        internal static void AttackPerson(ProjectUnitBase unit, Person person)
        {
            PersonAttacked?.Invoke(null, new PersonAttackedEventArgs(unit, person));
        }

        internal static void RemoveLine(ProjectLine line)
        {
            LineIsRemoved?.Invoke(null, new LineEventArgs(line));
        }

        public static void StartDecision(Decision decision)
        {
            DecisionIsStarted?.Invoke(null,new DecisionEventArgs(decision));
        }
    }
}