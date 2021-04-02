﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.BL
{

    public class Person: ISpeechSource
    {
        public const int MAX_SKILL_LEVEL = 16;

        private const int COMMIT_SPEED_BASE = 1;
        private const int ERROR_CHANCE_BASE = 50;
        private const float SKILLUP_SPEED_BASE = 0.01f;
        private const int EFFECT_CHANCE_BASE = 15;
        private const int ENERGY_BASE = 100;
        private const float RECOVERY_TIME_BASE = 8;
        private const int ENERGY_DOWN_SPEED_BASE = 1;
        private const int RECOVERY_SPEED_BASE = 1;
        private const int DAYLY_PAYMENT_BASE = 1;
        private const float COMMIT_POWER_BASE = 0.25f;
        private static float CRIT_COMMIT_CHANCE_BASE = 2.5f;
        private static float CRIT_COMMIT_MULTIPLICATOR_BASE = 2;

        private float _commitCounter;
        private float _speechCounter = Speech.SPEECH_COUNTER;
        

        public int EyeIndex { get; set; }
        public int FaceDecorIndex { get; set; }

        public string Name { get; set; }
        public float CommitSpeed { get; set; } = COMMIT_SPEED_BASE;

        public float CommitPower { get; set; } = COMMIT_POWER_BASE;

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

        public int? LineIndex { get; set; }
        public float EnergyDownSpeed { get; private set; } = ENERGY_DOWN_SPEED_BASE;
        public float RecoverySpeed { get; private set; } = RECOVERY_SPEED_BASE;

        public int DaylyPayment { get; set; } = DAYLY_PAYMENT_BASE;

        public void Update(ProjectUnitBase assignedUnit, float commitDeltaTime)
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

            if (assignedUnit is null)
            {
                return;
            }

            ProgressUnitSolving(assignedUnit, commitDeltaTime);
        }

        public void DaylyUpdate()
        {
            CheckForNewEffect();
        }

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
                            SkillUpSpeed *= 2f;
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
                        ErrorChance = 50 - 25 / 2;
                        CommitSpeed = 2;
                        break;

                    case TraitType.RapidDevelopment:
                        ErrorChance = 50 + 25 / 2;
                        CommitSpeed = 0.5f;
                        break;
                }
            }
        }

        private void ProgressUnitSolving(ProjectUnitBase unit, float commitDeltaTime)
        {
            _commitCounter += commitDeltaTime;

            const float baseCommitTimeSeconds = 2;
            var targetCommitCounter = baseCommitTimeSeconds * CommitSpeed;

            if (_commitCounter >= targetCommitCounter)
            {
                _commitCounter = 0;
                Commited?.Invoke(this, EventArgs.Empty);

                unit.ProcessCommit(this);

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
            }
        }
    }
}
