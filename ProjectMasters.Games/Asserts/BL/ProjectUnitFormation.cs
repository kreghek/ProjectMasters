using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.BL
{
    public class ProjectUnitFormation
    {
        public int ProjectMoneyEarning = 1000;
        public int ProjectAuthrityEarning = 100;
        public List<SolvedUnitInfo> SolvedUnits { get; } = new List<SolvedUnitInfo>();

        public List<ProjectLine> Lines { get; set; }

        public event EventHandler<UnitEventArgs> Added;
        public event EventHandler<UnitEventArgs> Removed;

        public static ProjectUnitFormation Instance = new ProjectUnitFormation();

        public ProjectUnitFormation()
        {
            Recreate();
        }

        public void Recreate()
        {
            Lines = new List<ProjectLine>();

            switch (Player.ProjectLevel)
            {
                case 0:
                    FillLines1();
                    break;

                case 1:
                    FillLines2();
                    break;
            }
        }

        private void FillLines1()
        {
            Lines.Add(new ProjectLine
            {
                Units = new List<ProjectUnitBase> {
                    new FeatureUnit{
                        Cost = 2,
                        LineIndex = 0,
                        QueueIndex = 0
                    }
                }
            });

            Lines.Add(new ProjectLine
            {
                Units = new List<ProjectUnitBase> {
                    new FeatureUnit{
                        Cost = 1,
                        LineIndex = 1,
                        QueueIndex = 0
                    }
                }
            });

            Lines.Add(new ProjectLine
            {
                Units = new List<ProjectUnitBase> {
                    new FeatureUnit{
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
                Units = new List<ProjectUnitBase> {
                    new FeatureUnit{
                        Cost = 2,
                        LineIndex = 0,
                        QueueIndex = 0
                    },
                       new FeatureUnit{
                        Cost = 2,
                        LineIndex = 0,
                        QueueIndex = 1
                    }
                }
            });

            Lines.Add(new ProjectLine
            {
                Units = new List<ProjectUnitBase> {
                    new FeatureUnit{
                        Cost = 1,
                        LineIndex = 1,
                        QueueIndex = 0
                    },
                    new FeatureUnit{
                        Cost = 2,
                        LineIndex = 1,
                        QueueIndex = 1
                    }
                }
            });

            Lines.Add(new ProjectLine
            {
                Units = new List<ProjectUnitBase> {
                    new FeatureUnit{
                        Cost = 0.8f,
                        LineIndex = 2,
                        QueueIndex = 0
                    },
                    new FeatureUnit{
                        Cost = 2f,
                        LineIndex = 2,
                        QueueIndex = 1
                    }
                }
            });
        }

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
            for (int i = 0; i < Lines[lineIndex].Units.Count; i++)
            {
                ProjectUnitBase unit1 = Lines[lineIndex].Units[i];
                unit1.QueueIndex = i;
            }

            Added?.Invoke(this, new UnitEventArgs { ProjectUnit = unit });
        }

        public void ResolveUnit(int lineIndex, ProjectUnitBase unit)
        {
            Lines[lineIndex].Units.Remove(unit);

            if (!Lines[lineIndex].Units.Any())
            {
                Lines.RemoveAt(lineIndex);
            }

            // reindex
            for (var lineIndex1 = 0; lineIndex1 < Lines.Count; lineIndex1++)
            {
                for (var i = 0; i < Lines[lineIndex1].Units.Count; i++)
                {
                    ProjectUnitBase unit1 = Lines[lineIndex1].Units[i];
                    unit1.QueueIndex = i;
                    unit1.LineIndex = lineIndex1;
                }

                SolvedUnits.Add(new SolvedUnitInfo { Cost = unit.Cost, TimeLog = unit.TimeLog });
            }

            Removed?.Invoke(this, new UnitEventArgs { ProjectUnit = unit });
        }
    }
}
