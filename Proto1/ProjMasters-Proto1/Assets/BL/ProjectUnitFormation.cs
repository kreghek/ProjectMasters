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
                if (TryGetClosestFree(Matrix, x, y, out var targetX, out var targetY))
                {
                    Matrix[targetX, targetY] = projectUnit;
                    Added?.Invoke(this, new UnitEventArgs { ProjectUnit = projectUnit, X = targetX, Y = targetY });
                }
            }
        }

        public void DeleteUnit(int x, int y)
        {
            if (Matrix[x, y] != null)
            {
                var unit = Matrix[x, y];
                Removed?.Invoke(this, new UnitEventArgs() { ProjectUnit = unit, X = x, Y = y });
            }
        }

        public void DeleteUnit(ProjectUnitBase unit)
        {
            var featureX = 0;
            var featureY = 0;
            for (int x = 0; x < Matrix.GetLength(0); x++)
            {
                for (int y = 0; y < Matrix.GetLength(1); y++)
                {
                    if (Matrix[x, y] == unit)
                    {
                        featureX = x;
                        featureY = y;
                        DeleteUnit(featureX, featureY);
                    }
                }
            }
        }

        private static bool TryGetClosestFree(ProjectUnitBase[,] matrix, int x, int y, out int targetX, out int targetY)
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (int xoff = -matrix.GetLength(0); xoff < matrix.GetLength(0); xoff++)
                {
                    for (int yoff = -matrix.GetLength(1); yoff < matrix.GetLength(1); yoff++)
                    {
                        var testX = x + xoff;
                        var testY = y + yoff;

                        if (!CheckBoundaries(testX, matrix.GetLength(0)))
                        {
                            continue;
                        }

                        if (!CheckBoundaries(testY, matrix.GetLength(1)))
                        {
                            continue;
                        }

                        if (matrix[testX, testY] != null)
                        {
                            continue;
                        }

                        targetX = testX;
                        targetY = testY;

                        return true;
                    }
                }
            }

            targetX = 0;
            targetY = 0;
            return false;
        }

        private static bool CheckBoundaries(int test, int boundary)
        {
            if (test < 0)
            {
                return false;
            }

            if (test >= boundary)
            {
                return false;
            }

            return true;
        }
    }

    public class UnitEventArgs : EventArgs
    {
        public ProjectUnitBase ProjectUnit { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
