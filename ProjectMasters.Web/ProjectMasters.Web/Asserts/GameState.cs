namespace ProjectMasters.Games
{
    using System;

    using Asserts;

    using Assets.BL;

    using Web.DTOs;

    public sealed class GameState
    {
        public GameState()
        {
            Player = new Player();
        }

        public Player Player { get; set; }
        public string UserId { get; set; }
        public bool Initialized { get; set; }
        public ProjectUnitFormation Project { get; set; }

        public bool Started { get; internal set; }
        public Team Team { get; set; }
        public TeamFactory TeamFactory { get; set; }

        public void AddEffect(Effect effect)
        {
            EffectIsAdded?.Invoke(null, new EffectEventArgs(effect));
        }

        public void DoUnitTakenDamage(ProjectUnitBase unit)
        {
            var dto = new UnitDto(unit);

            var args = new UnitTakenDamageEventArgs(dto);
            UnitTakenDamage?.Invoke(null, args);
        }

        public void LearnSkill(Skill skill)
        {
            SkillIsLearned?.Invoke(null, new SkillEventArgs(skill));
        }


        public void RemoveEffect(Effect effect)
        {
            EffectIsRemoved?.Invoke(null, new EffectEventArgs(effect));
        }

        public void RestPerson(Person person)
        {
            PersonIsRested?.Invoke(null, new PersonEventArgs(person));
        }

        public void StartDecision(Decision decision)
        {
            DecisionIsStarted?.Invoke(null, new DecisionEventArgs(decision));
        }

        public void TirePerson(Person person)
        {
            PersonIsTired?.Invoke(null, new PersonEventArgs(person));
        }

        internal void AssignPerson(ProjectLine line, Person person)
        {
            PersonAssigned?.Invoke(null, new PersonAssignedEventArgs(line, person));
        }

        internal void AttackPerson(ProjectUnitBase unit, Person person)
        {
            PersonAttacked?.Invoke(null, new PersonAttackedEventArgs(unit, person));
        }

        internal void RemoveLine(ProjectLine line)
        {
            LineIsRemoved?.Invoke(null, new LineEventArgs(line));
        }

        public event EventHandler<DecisionEventArgs> DecisionIsStarted;
        public event EventHandler<EffectEventArgs> EffectIsAdded;
        public event EventHandler<EffectEventArgs> EffectIsRemoved;
        public event EventHandler<LineEventArgs> LineIsRemoved;
        public event EventHandler<PersonAssignedEventArgs> PersonAssigned;
        public event EventHandler<PersonAttackedEventArgs> PersonAttacked;
        public event EventHandler<PersonEventArgs> PersonIsRested;
        public event EventHandler<PersonEventArgs> PersonIsTired;
        public event EventHandler<SkillEventArgs> SkillIsLearned;
        public event EventHandler<UnitTakenDamageEventArgs> UnitTakenDamage;
    }
}