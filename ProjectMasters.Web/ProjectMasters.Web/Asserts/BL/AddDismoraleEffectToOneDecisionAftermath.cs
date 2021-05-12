namespace Assets.BL
{
    using System;

    using ProjectMasters.Games;

    public sealed class AddDismoraleEffectToOneDecisionAftermath : DecisionAftermathBase
    {
        public override string Description =>
            "The one of eployees gain negative morale effect. It reduces effecient of employee.";

        private static Random Random => new Random(DateTime.Now.Millisecond);

        public override void Apply()
        {
            var selectedPerson = GetRandomPerson();

            var dismoraleEffect = CreateEffect();

            selectedPerson.Effects.Add(dismoraleEffect);
        }

        private Effect CreateEffect()
        {
            return new Effect
            {
                EffectType = EffectType.Despondency,
                Lifetime = Effect.MAX_DURATION * 3
            };
        }

        private static Person GetRandomPerson()
        {
            var persons = GameState.Team.Persons;
            var personIndex = Random.Next(0, persons.Length - 1);
            var selectedPerson = persons[personIndex];
            return selectedPerson;
        }
    }
}