using UnityEngine;

namespace Assets.BL
{
    public sealed class SubTaskUnit : ProjectUnitBase
    {
        public override ProjectUnitType Type => ProjectUnitType.SubTask;

        public override void ProcessCommit()
        {
            if (Random.Range(1, 100) >= 50)
            {
                TimeLog += 0.25f;
            }

            if (TimeLog >= Cost)
            {
                var formation = ProjectUnitFormation.Instance;
                formation.DeleteUnit(this);
                IsDead = true;
            }
        }
    }
}
