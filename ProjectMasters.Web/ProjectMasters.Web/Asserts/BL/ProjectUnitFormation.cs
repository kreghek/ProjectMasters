namespace Assets.BL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ProjectMasters.Games;

    public class ProjectUnitFormation
    {
        public static ProjectUnitFormation Instance = new ProjectUnitFormation();
        public int ProjectAuthrityEarning = 100;
        public int ProjectMoneyEarning = 1000;

        private ProjectUnitFormation()
        {
            Recreate();
        }

        public List<ProjectLine> Lines { get; set; }
        public List<SolvedUnitInfo> SolvedUnits { get; } = new List<SolvedUnitInfo>();

        public void AddUnitIntoLine(int lineIndex, int positionIndex, ProjectUnitBase unit)
        {
            unit.LineIndex = lineIndex;

            if (positionIndex >= Lines[lineIndex].Units.Count)
            {
                Lines[lineIndex].Units.Add(unit);
            }
            else
            {
                Lines[lineIndex].Units.Insert(positionIndex, unit);
            }

            // reindex
            for (var i = 0; i < Lines[lineIndex].Units.Count; i++)
            {
                var unit1 = Lines[lineIndex].Units[i];
                unit1.QueueIndex = i;
            }

            Added?.Invoke(this, new UnitEventArgs(unit));
        }

        public void ResolveUnit(int lineIndex, ProjectUnitBase unit)
        {
            unit.LineIndex = lineIndex;
            Lines[lineIndex].Units.Remove(unit);

            if (!Lines[lineIndex].Units.Any())
            {
                GameState.RemoveLine(Lines[lineIndex]);
                Lines.RemoveAt(lineIndex);
            }

            // reindex
            for (var lineIndex1 = 0; lineIndex1 < Lines.Count; lineIndex1++)
            {
                for (var i = 0; i < Lines[lineIndex1].Units.Count; i++)
                {
                    var unit1 = Lines[lineIndex1].Units[i];
                    unit1.QueueIndex = i;
                    unit1.LineIndex = lineIndex1;
                }

                SolvedUnits.Add(new SolvedUnitInfo { Cost = unit.Cost, TimeLog = unit.TimeLog });
            }

            Removed?.Invoke(this, new UnitEventArgs(unit));
        }

        private void FillLines1()
        {
            Lines.Add(new ProjectLine
            {
                Id = 0,
                Units = new List<ProjectUnitBase>
                {
                    new FeatureUnit
                    {
                        Id = UnitIdGenerator.GetId(),
                        Cost = 2,
                        LineIndex = 0,
                        QueueIndex = 0
                    }
                }
            });

            Lines.Add(new ProjectLine
            {
                Id = 1,
                Units = new List<ProjectUnitBase>
                {
                    new FeatureUnit
                    {
                        Id = UnitIdGenerator.GetId(),
                        Cost = 1,
                        LineIndex = 1,
                        QueueIndex = 0
                    }
                }
            });

            Lines.Add(new ProjectLine
            {
                Id = 2,
                Units = new List<ProjectUnitBase>
                {
                    new FeatureUnit
                    {
                        Id = UnitIdGenerator.GetId(),
                        Cost = 0.8f,
                        LineIndex = 2,
                        QueueIndex = 0
                    }
                }
            });
        }

        private void FillLines2()
        {
            Lines.Add(new ProjectLine
            {
                Units = new List<ProjectUnitBase>
                {
                    new FeatureUnit
                    {
                        Cost = 2,
                        LineIndex = 0,
                        QueueIndex = 0
                    },
                    new FeatureUnit
                    {
                        Cost = 2,
                        LineIndex = 0,
                        QueueIndex = 1
                    }
                }
            });

            Lines.Add(new ProjectLine
            {
                Units = new List<ProjectUnitBase>
                {
                    new FeatureUnit
                    {
                        Cost = 1,
                        LineIndex = 1,
                        QueueIndex = 0
                    },
                    new FeatureUnit
                    {
                        Cost = 2,
                        LineIndex = 1,
                        QueueIndex = 1
                    }
                }
            });

            Lines.Add(new ProjectLine
            {
                Units = new List<ProjectUnitBase>
                {
                    new FeatureUnit
                    {
                        Cost = 0.8f,
                        LineIndex = 2,
                        QueueIndex = 0
                    },
                    new FeatureUnit
                    {
                        Cost = 2f,
                        LineIndex = 2,
                        QueueIndex = 1
                    }
                }
            });
        }

        private void Recreate()
        {
            Lines = new List<ProjectLine>();
            FillLines1();

            //switch (Player.ProjectLevel)
            //{
            //    case 0:
            //        FillLines1();
            //        break;

            //    case 1:
            //        FillLines2();
            //        break;
            //}
        }

        public event EventHandler<UnitEventArgs> Added;

        public event EventHandler<UnitEventArgs> Removed;
    }
}