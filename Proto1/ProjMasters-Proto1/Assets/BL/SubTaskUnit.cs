﻿using System.Collections.Generic;

using UnityEngine;

namespace Assets.BL
{
    public sealed class SubTaskUnit : ProjectUnitBase
    {
        public override ProjectUnitType Type => ProjectUnitType.SubTask;

        public override void ProcessCommit(IEnumerable<Skill> skills)
        {
            var isSuccessfullCommit = RollCommitSuccess(skills);
            if (isSuccessfullCommit)
            {
                TimeLog += 0.25f;
                DoTakeDamage();
            }

            if (Random.Range(1, 100) >= 75)
            {
                var formation = ProjectUnitFormation.Instance;

                var errorUnit = CreateErrorUnit();

                if (Random.Range(1, 100) > 50)
                {
                    formation.AddUnitIntoLine(LineIndex, 0, errorUnit);
                }
                else
                {
                    formation.AddUnitIntoLine(LineIndex, QueueIndex + 1, errorUnit);
                }
            }

            if (TimeLog >= Cost)
            {
                var formation = ProjectUnitFormation.Instance;
                formation.DeleteUnit(LineIndex, this);
                IsDead = true;
            }
        }

        private ErrorUnit CreateErrorUnit()
        {
            var subTask = new ErrorUnit
            {
                Cost = Random.Range(4, 16)
            };

            return subTask;
        }
    }
}
