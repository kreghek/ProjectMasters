using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.BL
{
    public class Person: ISpeechSource
    {
        public const int MAX_SKILL_LEVEL = 16;

        private const int COMMIT_SPEED_BASE = 1;
        private const int ERROR_CHANCE_BASE = 50;
        private const float SKILLUP_SPEED_BASE = 0.001f;
        private const int EFFECT_CHANCE_BASE = 15;
        private const int ENERGY_BASE = 100;
        private const float RECOVERY_TIME_BASE = 8;
        private const int ENERGY_DOWN_SPEED_BASE = 1;
        private const int RECOVERY_SPEED_BASE = 1;
        private const int DAYLY_PAYMENT_BASE = 1;
        private const float COMMIT_POWER_BASE = 0.25f;
        private static float CRIT_COMMIT_CHANCE_BASE = 2.5f;
        private static float CRIT_COMMIT_MULTIPLICATOR_BASE = 2;
        public static float PROJECT_KNOWEDGE_INCREMENT = 0.25f;

        private float _speechCounter = Speech.SPEECH_COUNTER;

        public Act[] Acts { get; set; } = new Act[]
        {
            new Act{ ActTargetPattern = ActTargetPattern.ClosestUnit, Impact = ActImpact.Units, Position = ActPosition.First },
            new Act{ ActTargetPattern = ActTargetPattern.OneOfFirstHalf, Impact = ActImpact.Units, Position = ActPosition.Second },
        };

        public int EyeIndex { get; set; }
        public int FaceDecorIndex { get; set; }

        public string Name { get; set; }
        public float CommitSpeed { get; set; } = COMMIT_SPEED_BASE;

        public float CommitPower { get; set; } = COMMIT_POWER_BASE;

        public float ProjectKnowedgeCoef { get; set; } = 1;

        public float CritCommitChance { get; set; } = CRIT_COMMIT_CHANCE_BASE;

        public float CritCommitMultiplicator { get; set; } = CRIT_COMMIT_MULTIPLICATOR_BASE;

        public float ErrorChance { get; set; } = ERROR_CHANCE_BASE;
        public float SkillUpSpeed { get; set; } = SKILLUP_SPEED_BASE;

        public Skill[] Skills { get; set; }

        public TraitType[] Traits { get; set; }

        public List<Effect> Effects { get; } = new List<Effect>();

        public float EnergyCurrent { get; set; } = ENERGY_BASE;

        public float Energy { get; set; } = ENERGY_BASE;

        public float? RecoveryCounter { get; set; }

        public event EventHandler<EventArgs> Commited;
        public float EnergyDownSpeed { get; private set; } = ENERGY_DOWN_SPEED_BASE;
        public float RecoverySpeed { get; private set; } = RECOVERY_SPEED_BASE;

        public int DaylyPayment { get; set; } = DAYLY_PAYMENT_BASE;

        internal void Update(List<ProjectUnitBase> units, List<Person> assignedPersons, float commitDeltaTime)
        {
            HandleSpeechs(commitDeltaTime);

            ResetStats();
            HandleTraits();

            HandleCurrentEffects(commitDeltaTime);

            HandleEnergy(commitDeltaTime);
            if (RecoveryCounter != null)
            {
                return;
            }

            ProgressUnitSolving(units, assignedPersons, commitDeltaTime);
        }

        public void DaylyUpdate()
        {
            CheckForNewEffect();
        }

        public int FeatureCompleteCount { get; set; }
        public int SubTasksCompleteCount { get; set; }
        public int ErrorCompleteCount { get; set; }
        public int ErrorMadeCount { get; set; }

        private void HandleSpeechs(float commitDeltaTime)
        {
            if (_speechCounter > 0)
            {
                _speechCounter -= commitDeltaTime;
            }
            else
            {
                _speechCounter = Speech.SPEECH_COUNTER;

                if (Effects.Any(x => x.EffectType == EffectType.Despondency || x.EffectType == EffectType.Toxic || x.EffectType == EffectType.Procrastination))
                {
                    if (UnityEngine.Random.Range(1, 100) <= 15)
                    {
                        var rolledBadSpeechIndex = UnityEngine.Random.Range(0, SpeechCatalog.BadPersonSpeeches.Length);
                        var speechText = SpeechCatalog.BadPersonSpeeches[rolledBadSpeechIndex];
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

        private void HandleEnergy(float commitDeltaTime)
        {
            if (EnergyCurrent > 0)
            {
                EnergyCurrent -= commitDeltaTime * EnergyDownSpeed;
            }
            else
            {
                if (RecoveryCounter == null)
                {
                    RecoveryCounter = RECOVERY_TIME_BASE;
                }
                else
                {
                    RecoveryCounter -= commitDeltaTime * RecoverySpeed;
                    if (RecoveryCounter <= 0)
                    {
                        RecoveryCounter = null;
                        EnergyCurrent = Energy;
                    }
                }
            }
        }

        private void CheckForNewEffect()
        {
            if (Effects.Any())
            {
                return;
            }

            var newEffectRoll = UnityEngine.Random.Range(1, 100);

            if (newEffectRoll < EFFECT_CHANCE_BASE)
            {
                var effectTypeIndex = UnityEngine.Random.Range(0, 3);
                var effectDuration = UnityEngine.Random.Range(Effect.MIN_DURATION, Effect.MAX_DURATION);
                var effect = new Effect
                {
                    EffectType = (EffectType)effectTypeIndex,
                    Lifetime = effectDuration
                };

                Effects.Add(effect);
            }
        }

        private void ResetStats()
        {
            SkillUpSpeed = SKILLUP_SPEED_BASE;
            ErrorChance = ERROR_CHANCE_BASE;
            CommitSpeed = COMMIT_SPEED_BASE;
            CommitPower = COMMIT_POWER_BASE;
        }

        private void HandleCurrentEffects(float commitDeltaTime)
        {
            foreach (var effect in Effects.ToArray())
            {
                effect.Lifetime-= commitDeltaTime;
                if (effect.Lifetime <= 0)
                {
                    Effects.Remove(effect);
                }
                else
                {
                    switch (effect.EffectType)
                    {
                        case EffectType.Procrastination:
                            CommitSpeed *= 0.5f;
                            break;

                        case EffectType.Stream:
                            CommitSpeed *= 1.5f;
                            break;

                        case EffectType.Scattered:
                            ErrorChance *= 1.5f;
                            break;

                        case EffectType.Evrika:
                            SkillUpSpeed *= 5f;
                            break;

                        case EffectType.Despondency:
                            CommitPower *= 0.1f;
                            break;

                        case EffectType.Toxic:
                            CommitPower *= 0.1f;
                            break;
                    }
                }
            }
        }

        private void HandleTraits()
        {
            foreach (var trait in Traits)
            {
                switch (trait)
                {
                    case TraitType.CarefullDevelopment:
                        ErrorChance -= ERROR_CHANCE_BASE / 2;
                        CommitSpeed *= 0.5f;
                        break;

                    case TraitType.RapidDevelopment:
                        ErrorChance += ERROR_CHANCE_BASE / 2;
                        CommitSpeed *= 2f;
                        break;

                    case TraitType.FastLearning:
                        SkillUpSpeed *= 3;
                        ErrorChance += ERROR_CHANCE_BASE / 2;
                        break;

                    case TraitType.Apologet:
                        SkillUpSpeed *= 0.3f;
                        ErrorChance -= ERROR_CHANCE_BASE / 2;
                        break;
                }
            }
        }

        private void ProgressUnitSolving(List<ProjectUnitBase> units, List<Person> assignedPersons, float commitDeltaTime)
        {
            Act actTouse = null;
            foreach (var act in Acts)
            {
                var personIndex = assignedPersons.IndexOf(this);
                var groupDebuff = 0.8f / assignedPersons.Count;
                if (assignedPersons.Count == 1)
                {
                    groupDebuff = 1;
                }

                switch (act.Position)
                {
                    case ActPosition.First:
                        if (personIndex == 0)
                        {
                            act.Update(commitDeltaTime * CommitSpeed * groupDebuff);
                        }
                        break;

                    case ActPosition.Second:
                        if (personIndex == 1)
                        {
                            act.Update(commitDeltaTime * CommitSpeed * groupDebuff);
                        }
                        break;
                }

                if (actTouse == null && act.IsReadyToUse)
                {
                    actTouse = act;
                }
            }

            var unitsToAttack = new ProjectUnitBase[0];

            if (actTouse != null)
            {
                switch (actTouse.Impact)
                {
                    case ActImpact.Units:

                        switch (actTouse.ActTargetPattern)
                        {
                            case ActTargetPattern.ClosestUnit:
                                var closestUnit = units.FirstOrDefault();
                                unitsToAttack = new[] { closestUnit };
                                break;

                            case ActTargetPattern.OneOfFirstHalf:
                                var closestRandomUnit = units.Take(2).OrderBy(x => UnityEngine.Random.Range(1, 100)).FirstOrDefault();
                                unitsToAttack = new[] { closestRandomUnit };
                                break;
                        }

                        break;
                }

                if (unitsToAttack.Any() && actTouse != null)
                {
                    foreach (var unit in unitsToAttack)
                    {
                        const float MAX_AUTHORITY = 200f;
                        const float MIN_AUTHORITY = 0;
                        const float MAX_EFFECT = 1.1f;
                        const float MIN_EFFECT = 0.9f;

                        var currentAuthorityPercentage = (Player.Autority - MIN_AUTHORITY) / (MIN_AUTHORITY - MAX_AUTHORITY);

                        var authorityCoef = MIN_EFFECT + (MAX_EFFECT - MIN_EFFECT) * currentAuthorityPercentage;

                        var commitPower = CommitPower * ProjectKnowedgeCoef * authorityCoef;
                        var isCritical = false;

                        if (UnityEngine.Random.Range(1, 100) < CritCommitChance)
                        {
                            isCritical = true;
                        }

                        if (isCritical)
                        {
                            commitPower *= CritCommitMultiplicator;
                        }

                        unit.ProcessCommit(commitPower, isCritical, this);
                        actTouse.Reset();

                        // Improve used skills

                        foreach (var requiredSkillScheme in unit.RequiredSkills)
                        {
                            var usedSkill = Skills.SingleOrDefault(x => x.Scheme == requiredSkillScheme);
                            if (usedSkill is null)
                            {
                                var newSkill = new Skill
                                {
                                    Scheme = requiredSkillScheme,
                                    Level = 0
                                };

                                Skills = Skills.Concat(new[] { newSkill }).ToArray();
                            }
                            else
                            {
                                if (usedSkill.Level < MAX_SKILL_LEVEL)
                                {
                                    usedSkill.Level += SkillUpSpeed;
                                }
                                else
                                {
                                    usedSkill.Level = MAX_SKILL_LEVEL;
                                }
                            }
                        }

                        if (ProjectKnowedgeCoef <= 2)
                        {
                            ProjectKnowedgeCoef += SkillUpSpeed;
                        }
                    }

                    Commited?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
