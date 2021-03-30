using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Assets.BL
{
    public sealed class FeatureUnit : ProjectUnitBase
    {
        public override ProjectUnitType Type => ProjectUnitType.Feature;

        public float CostToDecompose { get; set; }

        public override void ProcessCommit()
        {
            if (Random.Range(1, 100) >= 50)
            {
                TimeLog += 0.25f;

                if (CostToDecompose < TimeLog)
                {
                    CostToDecompose = 0;

                    var subTaskCount = Random.Range(1, 3);
                    var subTasks = CreateSubTasks(this, subTaskCount);
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

    public sealed class SubTaskUnit : ProjectUnitBase
    {
        public override ProjectUnitType Type => ProjectUnitType.SubTask;

        public override void ProcessCommit()
        {
            if (Random.Range(1, 100) >= 50)
            {
                TimeLog += 0.25f;
            }
        }
    }
}
