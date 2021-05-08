using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.BL
{
    using ProjectMasters.Games;

    public abstract class ProjectUnitBase: ISpeechSource
    {
        public int Id { get; set; }
        private Random Random => new Random(DateTime.Now.Millisecond);

        protected float _speechCounter;
        private bool _isDead;
        public abstract ProjectUnitType Type { get; }
        public float Cost { get; set; }
        public float TimeLog { get; set; }
        public string[] RequiredMasteryItems { get; set; }

        public abstract void ProcessCommit(float commitPower, bool isCritical, Person person);

        public bool IsDead
        {
            get => _isDead;
            set
            {
                _isDead = value;
                GameState.KillUnit(this);
            }
        }

        public int LineIndex { get; set; }

        public int QueueIndex { get; set; }

        public event EventHandler<UnitTakeDamageEventArgs> TakeDamage;

        public bool IsGoalItem { get; set; }

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
            else
            {
                return 0;
            }
        }

        protected bool RollCommitSuccess(IEnumerable<Mastery> skills)
        {
            var effectiveCommitRoll = Random.Next(1, Person.MAX_SKILL_LEVEL);
            var successCommitRoll = GetSuccessCommitRoll(skills);

            var successfullCommit = effectiveCommitRoll <= successCommitRoll;
            return successfullCommit;
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
    }
}
