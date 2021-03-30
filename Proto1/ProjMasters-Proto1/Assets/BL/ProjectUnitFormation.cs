using System;

namespace Assets.BL
{
    public class ProjectUnitFormation
    {
        public ProjectUnitBase[,] Matrix = new ProjectUnitBase[10, 10];

        public event EventHandler<EventArgs> Added;
        public event EventHandler<EventArgs> Removed;

        public static ProjectUnitFormation Instance = new ProjectUnitFormation();

        public void AddUnitInClosestPosition(ProjectUnitBase projectUnit, int x, int y)
        {
            if (Matrix[x, y] is null)
            {
                Matrix[x, y] = projectUnit;
                Added?.Invoke(this, new UnitEventArgs { ProjectUnit = projectUnit, X = x, Y = y });
            }
            else
            {
                for (int i = 0; i < Matrix.GetLength(0); i++)
                {
                    /////////
                }
            }
        }
    }

    public class UnitEventArgs : EventArgs
    {
        public ProjectUnitBase ProjectUnit { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
