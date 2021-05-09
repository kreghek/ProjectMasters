namespace ProjectMasters.Games
{
    using System;

    using Assets.BL;

    public class UnitEventArgs : EventArgs
    {
        public UnitEventArgs(ProjectUnitBase unit)
        {
            Unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        public ProjectUnitBase Unit { get; }
    }
}