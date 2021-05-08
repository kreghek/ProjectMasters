namespace Assets.BL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ProjectMasters.Games;

    public class Person : ISpeechSource
    {
        private const float COMMIT_POWER_BASE = 0.25f;

        private const int COMMIT_SPEED_BASE = 1;
        private const int DAYLY_PAYMENT_BASE = 60;
        private const int EFFECT_CHANCE_BASE = 15;
        private const int ENERGY_BASE = 100;
        private const int ENERGY_DOWN_SPEED_BASE = 1;
        private const int ERROR_CHANCE_BASE = 50;
        public const int MAX_SKILL_LEVEL = 16;
        private const int RECOVERY_SPEED_BASE = 1;
        private const float RECOVERY_TIME_BASE = 8;
        private const float SKILLUP_SPEED_BASE = 0.001f;
        private static readonly float CHANGE_LINE_COUNTER_MULTIPLICATOR_BASE = 3;

        private static readonly float CRIT_COMMIT_CHANCE_BASE = 2.5f;
        private static readonly float CRIT_COMMIT_MULTIPLICATOR_BASE = 2;
        public static float PROJECT_KNOWEDGE_INCREMENT = 0.25f;
        private float _changeLineCounter;

        private float _speechCounter = Speech.SPEECH_COUNTER;
        public Skill ActiveSkill { get; set; }

        public Act[] Acts { get; set; } =
        {
            new Act
            {
                ActTargetPattern = ActTargetPattern.ClosestUnit, Impact = ActImpact.Units, Position = ActPosition.First
            },
            new Act
            {
                ActTargetPattern = ActTargetPattern.OneOfFirstHalf, Impact = ActImpact.Units,
                Position = ActPosition.Second
            },
        };

        public float CommitPower { get; set; } = COMMIT_POWER_BASE;
        public float CommitSpeed { get; set; } = COMMIT_SPEED_BASE;

        public float CritCommitChance { get; set; } = CRIT_COMMIT_CHANCE_BASE;

        public float CritCommitMultiplicator { get; set; } = CRIT_COMMIT_MULTIPLICATOR_BASE;

        public int DaylyPayment { get; set; } = DAYLY_PAYMENT_BASE;

        public List<Effect> Effects { get; } = new List<Effect>();

        public float Energy { get; set; } = ENERGY_BASE;

        public float EnergyCurrent { get; set; } = ENERGY_BASE;
        public float EnergyDownSpeed { get; } = ENERGY_DOWN_SPEED_BASE;

        public float ErrorChance { get; set; } = ERROR_CHANCE_BASE;
        public int ErrorCompleteCount { get; set; }
        public int ErrorMadeCount { get; set; }

        public int EyesIndex { get; set; }
        public int HairIndex { get; set; }
        public int MouthIndex { get; set; }
        public int FaceDecorIndex { get; set; }

        public int FeatureCompleteCount { get; set; }
        public int Id { get; internal set; }

        public List<Mastery> MasteryLevels { get; set; } = new List<Mastery>();

        public string Name { get; set; }

        public float ProjectKnowedgeCoef { get; set; } = 1;
        private Random Random => new Random(DateTime.Now.Millisecond);

        public float? RecoveryCounter { get; set; }
        public float RecoverySpeed { get; } = RECOVERY_SPEED_BASE;

        public Skill[] Skills { get; set; }
        public float SkillUpSpeed { get; set; } = SKILLUP_SPEED_BASE;
        public int SubTasksCompleteCount { get; set; }

        public TraitType[] Traits { get; set; }

        public event EventHandler<EventArgs> Commited;

        public void DaylyUpdate()
        {
            CheckForNewEffect();
            SelectRandomActiveSkill();
        }

        internal void SetChangeLineCounter()
        {
            _changeLineCounter = CHANGE_LINE_COUNTER_MULTIPLICATOR_BASE * CommitSpeed;
        }

        internal void Update(List<ProjectUnitBase> units, List<Person> assignedPersons, float commitDeltaTime)
        {
            HandleSpeechs(commitDeltaTime);

            ResetStats();
            HandleTraits();

            HandleCurrentEffects(commitDeltaTime);

            HandleEnergy(commitDeltaTime);
            if (RecoveryCounter != null)
                return;

            if (_changeLineCounter > 0)
            {
                _changeLineCounter -= commitDeltaTime;
                return;
            }

            CalcMasteryLevels();

            ProgressUnitSolving(units, assignedPersons, commitDeltaTime);
        }

        private void CalcMasteryLevels()
        {
            foreach (var masteryLevel in MasteryLevels)
            {
                masteryLevel.Level = 0;
            }

            foreach (var skill in Skills)
            {
                foreach (var skillMasteryTag in skill.Scheme.MasteryTags)
                {
                    var mastery = MasteryLevels.SingleOrDefault(x => x.Sid == skillMasteryTag);
                    if (mastery is null)
                    {
                        mastery = new Mastery
                        {
                            Sid = skillMasteryTag,
                            Level = 0
                        };

                        MasteryLevels.Add(mastery);
                    }

                    mastery.Level += skill.Scheme.MasteryIncrenemt;
                }
            }
        }

        private void CheckForNewEffect()
        {
            if (Effects.Any())
                return;

            var newEffectRoll = Random.Next(1, 100);

            if (newEffectRoll < EFFECT_CHANCE_BASE)
            {
                var effectTypeIndex = Random.Next(0, 3);
                var effectDuration = Random.Next((int)Effect.MIN_DURATION, (int)Effect.MAX_DURATION);
                var effect = new Effect
                {
                    Id = EffectIdGenerator.GetId(),
                    EffectType = (EffectType)effectTypeIndex,
                    Lifetime = effectDuration
                };

                Effects.Add(effect);
                GameState.AddEffect(effect);
            }
        }

        private void HandleCurrentEffects(float commitDeltaTime)
        {
            foreach (var effect in Effects.ToArray())
            {
                effect.Lifetime -= commitDeltaTime;
                if (effect.Lifetime <= 0)
                {
                    Effects.Remove(effect);
                    GameState.RemoveEffect(effect);
                }
                else
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
                    GameState.TirePerson(this);
                }
                else
                {
                    RecoveryCounter -= commitDeltaTime * RecoverySpeed;
                    if (RecoveryCounter <= 0)
                    {
                        RecoveryCounter = null;
                        EnergyCurrent = Energy;
                        GameState.RestPerson(this);
                    }
                }
            }
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

                if (Effects.Any(x =>
                    x.EffectType == EffectType.Despondency || x.EffectType == EffectType.Toxic ||
                    x.EffectType == EffectType.Procrastination))
                    if (Random.Next(1, 100) <= 15)
                    {
                        var rolledBadSpeechIndex = Random.Next(0, SpeechCatalog.BadPersonSpeeches.Length);
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

        private void ProgressUnitSolving(List<ProjectUnitBase> units, List<Person> assignedPersons,
            float commitDeltaTime)
        {
            Act actTouse = null;
            foreach (var act in Acts)
            {
                var personIndex = assignedPersons.IndexOf(this);
                var groupDebuff = 0.8f / assignedPersons.Count;
                if (assignedPersons.Count == 1)
                    groupDebuff = 1;

                switch (act.Position)
                {
                    case ActPosition.First:
                        if (personIndex == 0)
                            act.Update(commitDeltaTime * CommitSpeed * groupDebuff);
                        break;

                    case ActPosition.Second:
                        if (personIndex == 1)
                            act.Update(commitDeltaTime * CommitSpeed * groupDebuff);
                        break;
                }

                if (actTouse == null && act.IsReadyToUse)
                    actTouse = act;
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
                                var closestRandomUnit =
                                    units.Take(2).OrderBy(x => Random.Next(1, 100)).FirstOrDefault();
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

                        var currentAuthorityPercentage =
                            (Player.Autority - MIN_AUTHORITY) / (MIN_AUTHORITY - MAX_AUTHORITY);

                        var authorityCoef = MIN_EFFECT + (MAX_EFFECT - MIN_EFFECT) * currentAuthorityPercentage;

                        var commitPower = CommitPower * ProjectKnowedgeCoef * authorityCoef;
                        var isCritical = false;

                        if (Random.Next(1, 100) < CritCommitChance)
                            isCritical = true;

                        if (isCritical)
                            commitPower *= CritCommitMultiplicator;

                        GameState.AttackPerson(unit,this);
                        unit.ProcessCommit(commitPower, isCritical, this);
                        actTouse.Reset();

                        if (ProjectKnowedgeCoef <= 2)
                            ProjectKnowedgeCoef += SkillUpSpeed;
                    }

                    Commited?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void ResetStats()
        {
            SkillUpSpeed = SKILLUP_SPEED_BASE;
            ErrorChance = ERROR_CHANCE_BASE;
            CommitSpeed = COMMIT_SPEED_BASE;
            CommitPower = COMMIT_POWER_BASE;
        }

        private void SelectRandomActiveSkill()
        {
            var availableSkills = new List<Skill>();
            foreach (var skillScheme in SkillCatalog.AllSchemes)
            {
                if (skillScheme.RequiredParentsSids is null || !skillScheme.RequiredParentsSids.Any())
                {
                    var knownSkill = Skills.SingleOrDefault(x => x.Scheme == skillScheme);
                    if (knownSkill is null)
                    {
                        availableSkills.Add(new Skill { Scheme = skillScheme });
                    }
                    else
                    {
                        if (!knownSkill.IsLearnt)
                            availableSkills.Add(knownSkill);
                    }
                }
                else
                {
                    var learnedParents = Skills
                        .Where(x => x.IsLearnt && skillScheme.RequiredParentsSids.Contains(x.Scheme.Sid)).ToList();
                    var remainsLearnedParents = skillScheme.RequiredParentsSids
                        .Except(learnedParents.Select(x => x.Scheme.Sid)).ToList();
                    if (!remainsLearnedParents.Any())
                    {
                        // dups

                        var knownSkill = Skills.SingleOrDefault(x => x.Scheme == skillScheme);
                        if (knownSkill is null)
                        {
                            availableSkills.Add(new Skill { Scheme = skillScheme });
                        }
                        else
                        {
                            if (!knownSkill.IsLearnt)
                                availableSkills.Add(knownSkill);
                        }

                        // end dups
                    }
                }
            }

            //Select random available skill to learn
            if (availableSkills.Any())
            {
                var rolledSkillIndex = Random.Next(0, availableSkills.Count);
                ActiveSkill = availableSkills[rolledSkillIndex];
            }
        }
    }
}