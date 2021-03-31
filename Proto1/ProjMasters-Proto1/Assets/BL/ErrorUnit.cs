using UnityEngine;

namespace Assets.BL
{
    public sealed class ErrorUnit : ProjectUnitBase
    {
        public override ProjectUnitType Type => ProjectUnitType.Error;

        public override void ProcessCommit()
        {
            if (Random.Range(1, 100) >= 50)
            {
                TimeLog += 0.25f;
                DoTakeDamage();
            }

            if (Random.Range(1, 100) >= 95)
            {
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

                var errorUnit = CreateErrorUnit();


                formation.AddUnitInClosestPosition(errorUnit, featureX, featureY);
            }

            if (TimeLog >= Cost)
            {
                var formation = ProjectUnitFormation.Instance;
                formation.DeleteUnit(this);
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
