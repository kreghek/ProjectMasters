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

        public override void ProcessCommit()
        {
            var formation = ProjectUnitFormation.Instance;

            if (Random.Range(1, 100) >= 50)
            {
                TimeLog += 0.25f;
                CostToDecompose -= 0.25f;

                DoTakeDamage();

                if (CostToDecompose <= 0)
                {
                    RefreshCostToDecompose();

                    var subTaskCount = Random.Range(1, _maxSubTask + 1);
                    var subTasks = CreateSubTasks(this, subTaskCount);

                    foreach (var subTask in subTasks)
                    {
                        formation.AddUnitIntoLine(LineIndex, 0, subTask);
                    }
                }

                if (TimeLog >= Cost)
                {
                    formation.DeleteUnit(LineIndex, this);
                    IsDead = true;
                }
            }
        }

        private IEnumerable<ProjectUnitBase> CreateSubTasks(ProjectUnitBase feature, int subTaskCount)
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
