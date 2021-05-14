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

        public bool Initialized { get; set; }

        public Player Player { get; set; }
        public ProjectUnitFormation Project { get; set; }

        public bool Started { get; internal set; }
        public Team Team { get; set; }
        public TeamFactory TeamFactory { get; set; }
        public string UserId { get; set; }

        public void AddEffect(Effect effect)
        {
            EffectIsAdded?.Invoke(this, new EffectEventArgs(effect));
        }

        public void DoUnitTakenDamage(ProjectUnitBase unit)
        {
            var dto = new UnitDto(unit);

            var args = new UnitTakenDamageEventArgs(dto);
            UnitTakenDamage?.Invoke(this, args);
        }

        public void LearnSkill(Skill skill)
        {
            SkillIsLearned?.Invoke(this, new SkillEventArgs(skill));
        }


        public void RemoveEffect(Effect effect)
        {
            EffectIsRemoved?.Invoke(this, new EffectEventArgs(effect));
        }

        public void RestPerson(Person person)
        {
            PersonIsRested?.Invoke(this, new PersonEventArgs(person));
        }

        public void StartDecision(Decision decision)
        {
            DecisionIsStarted?.Invoke(this, new DecisionEventArgs(decision));
        }

        public void TirePerson(Person person)
        {
            PersonIsTired?.Invoke(this, new PersonEventArgs(person));
        }

        internal void AssignPerson(ProjectLine line, Person person)
        {
            PersonAssigned?.Invoke(this, new PersonAssignedEventArgs(line, person));
        }

        internal void AttackPerson(ProjectUnitBase unit, Person person)
        {
            PersonAttacked?.Invoke(this, new PersonAttackedEventArgs(unit, person));
        }

        internal void RemoveLine(ProjectLine line)
        {
            LineIsRemoved?.Invoke(this, new LineEventArgs(line));
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