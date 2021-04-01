using System;

namespace Assets.BL
{
    public class UnitEventArgs : EventArgs
    {
        public ProjectUnitBase ProjectUnit { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
