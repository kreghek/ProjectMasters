using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Assets.BL
{
    public sealed class FeatureUnit : ProjectUnitBase
    {
        private const float _minDecomposeCost = 1;
        private const float _maxDecomposeCost = 2;
        private const int _maxSubTask = 5;

        public FeatureUnit()
        {
            RefreshCostToDecompose();
        }

        private void RefreshCostToDecompose()
        {
            CostToDecompose = Random.Range(_minDecomposeCost, _maxDecomposeCost);
        }

        public override ProjectUnitType Type => ProjectUnitType.Feature;

        public float CostToDecompose { get; private set; }

        public override void ProcessCommit(Person person)
        {
            var formation = ProjectUnitFormation.Instance;
            var successfullCommit = RollCommitSuccess(person.Skills);
            if (successfullCommit)
            {
                var commitPower = person.CommitPower;
                var isCritical = false;

                if (Random.Range(1, 100) < person.CritCommitChance)
                {
                    isCritical = true;
                }

                if (isCritical)
                {
                    commitPower *= person.CritCommitMultiplicator;
                }

                TimeLog += commitPower;
                CostToDecompose -= commitPower;

                DoTakeDamage(commitPower, isCritical);

                if (CostToDecompose <= 0)
                {
                    RefreshCostToDecompose();

                    var subTaskCount = Random.Range(1, _maxSubTask + 1);
                    var subTasks = CreateSubTasks(subTaskCount);

                    foreach (var subTask in subTasks)
                    {
                        if (Random.Range(1, 100) > 50)
                        {
                            formation.AddUnitIntoLine(LineIndex, 0, subTask);
                        }
                        else
                        {
                            formation.AddUnitIntoLine(LineIndex, QueueIndex + 1, subTask);
                        }
                    }
                }

                if (TimeLog >= Cost)
                {
                    formation.ResolveUnit(LineIndex, this);
                    IsDead = true;
                }
                else
                {
                    if ((Cost - TimeLog) > 8)
                    {
                        HandleSpeechs(Time.deltaTime);
                    }
                }
            }
        }

        private IEnumerable<ProjectUnitBase> CreateSubTasks(int subTaskCount)
        {
            for (int i = 0; i < subTaskCount; i++)
            {
                var subTask = new SubTaskUnit
                {
                    Cost = Random.Range(4, 16)
                };

                var randomizedSkills = SkillSchemeCatalog.SkillSchemes.OrderBy(x1 => Random.Range(1, 100));
                var requiredSkillCount = Random.Range(1, SkillSchemeCatalog.SkillSchemes.Length);
                var requiredSkills = randomizedSkills.Take(requiredSkillCount);
                subTask.RequiredSkills = requiredSkills.ToArray();

                yield return subTask;
            }
        }
    }
}
