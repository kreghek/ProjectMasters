using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Assets.BL
{
    public sealed class SubTaskUnit : ProjectUnitBase
    {
        public override ProjectUnitType Type => ProjectUnitType.SubTask;

        public override void ProcessCommit(float commitPower, bool isCritical, Person person)
        {
            var isSuccessfullCommit = RollCommitSuccess(person.MasteryLevels);
            if (isSuccessfullCommit)
            {
                TimeLog += commitPower;
                DoTakeDamage(commitPower, isCritical);
            }

            if (Cost * 0.5f < TimeLog && UnityEngine.Random.Range(1, 100) < person.ErrorChance)
            {
                var formation = ProjectUnitFormation.Instance;

                var errorCount = UnityEngine.Random.Range(1, 5);
                for (int errorIndex = 0; errorIndex < errorCount; errorIndex++)
                {
                    var errorUnit = CreateErrorUnit();

                    if (UnityEngine.Random.Range(1, 100) > 50)
                    {
                        formation.AddUnitIntoLine(LineIndex, 0, errorUnit);
                    }
                    else
                    {
                        formation.AddUnitIntoLine(LineIndex, QueueIndex + 1, errorUnit);
                    }
                }

                person.ErrorMadeCount += errorCount;
            }

            if (TimeLog >= Cost)
            {
                var formation = ProjectUnitFormation.Instance;
                formation.ResolveUnit(LineIndex, this);
                person.SubTasksCompleteCount++;
                IsDead = true;
                person.ProjectKnowedgeCoef += Person.PROJECT_KNOWEDGE_INCREMENT + Person.PROJECT_KNOWEDGE_INCREMENT * person.SkillUpSpeed;



                var skillToUp = person.ActiveSkill;
                if (skillToUp != null)
                {
                    // Add active skill to Skills collection
                    // If it is not there. Union checks duplicates.
                    person.Skills = person.Skills.Union(new[] { skillToUp }).ToArray();

                    if (skillToUp.Jobs is null)
                    {
                        skillToUp.Jobs = Array.Empty<Job>();
                    }

                    var affectedJobsFromScheme = skillToUp.Scheme.RequiredJobs.Where(x => RequiredMasteryItems.Contains(x.MasteryScheme) && x.CompleteSubTasksAmount > 0);

                    foreach (var affectedJobScheme in affectedJobsFromScheme)
                    {
                        var affectedJobInSkillToUp = skillToUp.Jobs.SingleOrDefault(x => x.Scheme == affectedJobScheme);
                        if (affectedJobInSkillToUp is null)
                        {
                            affectedJobInSkillToUp = new Job
                            {
                                Scheme = affectedJobScheme
                            };

                            skillToUp.Jobs = skillToUp.Jobs.Concat(new[] { affectedJobInSkillToUp }).ToArray();
                        }

                        affectedJobInSkillToUp.CompleteSubTasksAmount++;
                    }

                    var isSkillArchieved = true;
                    var notStartedJobs = skillToUp.Scheme.RequiredJobs.Except(skillToUp.Jobs.Select(x => x.Scheme));
                    if (!notStartedJobs.Any())
                    {
                        foreach (var job in skillToUp.Jobs)
                        {
                            if (job.CompleteSubTasksAmount < job.Scheme.CompleteSubTasksAmount)
                            {
                                isSkillArchieved = false;
                                break;
                            }

                            if (job.CompleteErrorsAmount < job.Scheme.CompleteErrorsAmount)
                            {
                                isSkillArchieved = false;
                                break;
                            }

                            if (job.FeatureDecomposesAmount < job.Scheme.FeatureDecomposesAmount)
                            {
                                isSkillArchieved = false;
                                break;
                            }
                        }
                    }

                    if (isSkillArchieved)
                    {
                        // Skill are in skills collection already.
                        skillToUp.IsLearnt = true;
                        person.ActiveSkill = null;
                    }
                }
            }
            else
            {
                if ((Cost - TimeLog) > 8)
                {
                    HandleSpeechs(Time.deltaTime);
                }
            }
        }

        private ErrorUnit CreateErrorUnit()
        {
            var subTask = new ErrorUnit
            {
                Cost = UnityEngine.Random.Range(1, 9),
                RequiredMasteryItems = RequiredMasteryItems
            };

            return subTask;
        }
    }
}
