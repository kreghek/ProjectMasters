﻿using UnityEngine;

namespace Assets.BL
{
    public sealed class ErrorUnit : ProjectUnitBase
    {
        public override ProjectUnitType Type => ProjectUnitType.Error;

        public override void ProcessCommit(Person person)
        {
            var isSuccessfullCommit = RollCommitSuccess(person.Skills);
            if (isSuccessfullCommit)
            {
                TimeLog += person.CommitPower;
                DoTakeDamage();
            }

            if (Random.Range(1, 100) < person.ErrorChance)
            {
                var formation = ProjectUnitFormation.Instance;

                var errorUnit = CreateErrorUnit();

                formation.AddUnitIntoLine(LineIndex, 0, errorUnit);
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
