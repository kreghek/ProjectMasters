﻿using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Assets.BL
{
    public sealed class FeatureUnit : ProjectUnitBase
    {
        private const float _minDecomposeCost = 1;
        private const float _maxDecomposeCost = 2;
        private const int _maxSubTask = 3;

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
            if (Random.Range(1, 100) >= 50)
            {
                TimeLog += 0.25f;
                CostToDecompose -= 0.25f;

                DoTakeDamage();

                if (CostToDecompose <= 0)
                {
                    RefreshCostToDecompose();

                    var subTaskCount = Random.Range(1, _maxSubTask);
                    var subTasks = CreateSubTasks(this, subTaskCount);

                    var featureX = 0;
                    var featureY = 0;
                    var formation = ProjectUnitFormation.Instance;
                    for (int x = 0; x < formation.Matrix.GetLength(0); x++)
                    {
                        for (int y = 0; y < formation.Matrix.GetLength(1); y++)
                        {
                            if (formation.Matrix[x, y] == this)
                            {
                                featureX = x;
                                featureY = y;
                            }
                        }
                    }

                    foreach (var subTask in subTasks)
                    {
                        formation.AddUnitInClosestPosition(subTask, featureX, featureY);
                    }
                }

                if (TimeLog >= Cost)
                {
                    var formation = ProjectUnitFormation.Instance;
                    formation.DeleteUnit(this);
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
