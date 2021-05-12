namespace Assets.BL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ProjectMasters.Games;

    public abstract class ProjectUnitBase : ISpeechSource
    {
        protected float _speechCounter;
        public float Cost { get; set; }

        public int Id { get; set; }

        public bool IsDead { get; set; }

        public bool IsGoalItem { get; set; }

        public int LineIndex { get; set; }

        public int QueueIndex { get; set; }
        public string[] RequiredMasteryItems { get; set; }
        public float TimeLog { get; set; }
        public abstract ProjectUnitType Type { get; }

        private static Random Random => new Random(DateTime.Now.Millisecond);

        public abstract void ProcessCommit(float commitPower, bool isCritical, Person person, GameState gameState);

        protected void DoTakeDamage(float damage, bool isCritical)
        {
            TakeDamage?.Invoke(this, new UnitTakeDamageEventArgs { Damage = damage, IsCrit = isCritical });
        }

        protected float GetSuccessCommitRoll(IEnumerable<Mastery> personSkills)
        {
            var usedSkills = personSkills.Where(x => RequiredMasteryItems.Contains(x.Sid)).ToArray();
            if (usedSkills.Any())
            {
                return usedSkills.Min(x => x.Level);
            }

            return 0;
        }

        protected void HandleSpeechs(float commitDeltaTime)
        {
            if (_speechCounter > 0)
            {
                _speechCounter -= commitDeltaTime;
            }
            else
            {
                _speechCounter = Speech.SPEECH_COUNTER;

                if (Random.Next(1, 100) <= 15)
                {
                    var rolledBadSpeechIndex = Random.Next(0, SpeechCatalog.UnitTauntSpeeches.Length);
                    var speechText = SpeechCatalog.UnitTauntSpeeches[rolledBadSpeechIndex];
                    var speech = new Speech
                    {
                        Source = this,
                        Text = speechText
                    };

                    SpeechPool.AddSpeech(speech);
                }
            }
        }

        protected bool RollCommitSuccess(IEnumerable<Mastery> skills)
        {
            var effectiveCommitRoll = Random.Next(1, Person.MAX_SKILL_LEVEL);
            var successCommitRoll = GetSuccessCommitRoll(skills);

            var successfullCommit = effectiveCommitRoll <= successCommitRoll;
            return successfullCommit;
        }

        public event EventHandler<UnitTakeDamageEventArgs> TakeDamage;
    }
}