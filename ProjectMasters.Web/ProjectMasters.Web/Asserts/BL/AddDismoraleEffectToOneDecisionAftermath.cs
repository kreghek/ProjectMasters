
namespace Assets.BL
{
    using System;

    public sealed class AddDismoraleEffectToOneDecisionAftermath : DecisionAftermathBase
    {
        public override string Description => "The one of eployees gain negative morale effect. It reduces effecient of employee.";
        private static Random Random => new Random(DateTime.Now.Millisecond);
        public override void Apply()
        {
            throw new NotImplementedException();
            //Person selectedPerson = GetRandomPerson();

            //var dismoraleEffect = CreateEffect();

            //selectedPerson.Effects.Add(dismoraleEffect);
        }

        //private static Person GetRandomPerson()
        //{
        //    var persons = Team.Persons;
        //    var personIndex = Random.Next(0, persons.Length - 1);
        //    var selectedPerson = persons[personIndex];
        //    return selectedPerson;
        //}

        private Effect CreateEffect()
        {
            return new Effect
            {
                EffectType = EffectType.Despondency,
                Lifetime = Effect.MAX_DURATION * 3
            };
        }
    }
}
