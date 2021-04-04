﻿using System;
using System.Collections.Generic;

namespace Assets.BL
{
    public class ProjectUnitFormation
    {
        public List<SolvedUnitInfo> SolvedUnits { get; } = new List<SolvedUnitInfo>();

        public List<ProjectLine> Lines { get; }

        public event EventHandler<UnitEventArgs> Added;
        public event EventHandler<UnitEventArgs> Removed;

        public static ProjectUnitFormation Instance = new ProjectUnitFormation();

        public ProjectUnitFormation()
        {
            Lines = new List<ProjectLine>();

            FillLines();
        }

        private void FillLines()
        {
            Lines.Add(new ProjectLine
            {
                Units = new List<ProjectUnitBase> {
                    new FeatureUnit{
                        Cost = 2,
                        LineIndex = 0,
                        QueueIndex = 0,
                        RequiredSkills = new[]{
                            SkillSchemeCatalog.SkillSchemes[0]
                        }
                    }
                }
            });

            Lines.Add(new ProjectLine
            {
                Units = new List<ProjectUnitBase> {
                    new FeatureUnit{
                        Cost = 6,
                        LineIndex = 1,
                        QueueIndex = 0,
                        RequiredSkills = new[]{
                            SkillSchemeCatalog.SkillSchemes[0],
                            SkillSchemeCatalog.SkillSchemes[1]
                        }
                    },
                    new FeatureUnit{
                        Cost = 1,
                        LineIndex = 1,
                        QueueIndex = 1,
                        RequiredSkills = new[]{
                            SkillSchemeCatalog.SkillSchemes[1]
                        }
                    }
                }
            });

            Lines.Add(new ProjectLine
            {
                Units = new List<ProjectUnitBase> {
                    new FeatureUnit{
                        Cost = 0.8f,
                        LineIndex = 2,
                        QueueIndex = 0,
                        RequiredSkills = new[]{
                            SkillSchemeCatalog.SkillSchemes[0]
                        }
                    }
                }
            });
        }

        public void AddUnitIntoLine(int lineIndex, int positionIndex, ProjectUnitBase unit)
        {
            unit.LineIndex = lineIndex;

            Lines[lineIndex].Units.Insert(positionIndex, unit);

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

            // reindex
            for (int i = 0; i < Lines[lineIndex].Units.Count; i++)
            {
                ProjectUnitBase unit1 = Lines[lineIndex].Units[i];
                unit1.QueueIndex = i;
            }

            SolvedUnits.Add(new SolvedUnitInfo { Cost = unit.Cost, TimeLog = unit.TimeLog });

            Removed?.Invoke(this, new UnitEventArgs { ProjectUnit = unit });
        }
    }
}
