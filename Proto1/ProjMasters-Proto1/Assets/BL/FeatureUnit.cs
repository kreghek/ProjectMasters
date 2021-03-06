﻿using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Assets.BL
{
    public sealed class FeatureUnit : ProjectUnitBase
    {
        private const float _minDecomposeCost = 1;
        private const float _maxDecomposeCost = 2;
        private const int _maxSubTask = 5;
        private const int MIN_SUBTASK_COST = 1;
        private const int MAX_SUBTASK_COST = 2;

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

        public override void ProcessCommit(float commitPower, bool isCritical,Person person)
        {
            var formation = ProjectUnitFormation.Instance;
            var successfullCommit = Random.Range(1, 16) <= 8;
            if (successfullCommit)
            {
                TimeLog += commitPower;
                CostToDecompose -= commitPower;

                DoTakeDamage(commitPower, isCritical);

                if (CostToDecompose <= 0 || TimeLog >= Cost)
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

                    person.FeatureCompleteCount++;

                    person.ProjectKnowedgeCoef += Person.PROJECT_KNOWEDGE_INCREMENT + Person.PROJECT_KNOWEDGE_INCREMENT * person.SkillUpSpeed;
                }

                if (TimeLog >= Cost)
                {
                    formation.ResolveUnit(LineIndex, this);
                    IsDead = true;

                    person.ProjectKnowedgeCoef += Person.PROJECT_KNOWEDGE_INCREMENT + Person.PROJECT_KNOWEDGE_INCREMENT * person.SkillUpSpeed;
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
                    Cost = Random.Range(MIN_SUBTASK_COST, MAX_SUBTASK_COST + 1)
                };

                var randomizedMasteries = new[] { SkillCatalog.MasterySids.BackendMastery, SkillCatalog.MasterySids.FrontendMastery }.OrderBy(x1 => Random.Range(1, 100));
                var requiredMasteryCount = Random.Range(1, randomizedMasteries.Count() + 1);
                var requiredMasteries = randomizedMasteries.Take(requiredMasteryCount);
                subTask.RequiredMasteryItems = requiredMasteries.ToArray();

                yield return subTask;
            }
        }
    }
}
