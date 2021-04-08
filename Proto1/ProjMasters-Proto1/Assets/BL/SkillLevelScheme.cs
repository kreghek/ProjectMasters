using System.Linq;

namespace Assets.BL
{
    public class SkillLevelScheme
    {
        public string[] RequiredParentsSids { get; set; }
        public JobScheme[] RequiredJobs { get; set; }
        public float MasterIncrenemt { get; set; }
        public MasteryScheme MasteryScheme { get; set; }
    }

    public static class SkillCatalog
    {
        public static SkillScheme[] AllSchemes { get; } = new SkillScheme[] {
            new SkillScheme{
                Sid = "csharp-foundations",
                Display = "C# Foundations",
                Levels = new[]{ 
                    new SkillLevelScheme{ 
                        MasterIncrenemt = 1f,
                        MasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                        RequiredJobs = new []{ 
                            new JobScheme{
                                MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                                CompleteSubTasksAmount = 10
                            }
                        }
                    }
                }
            },
            new SkillScheme{
                Sid = "asp-net-core-foundations",
                Display = "ASP.NET Core Foundations",
                Levels = new[]{
                    new SkillLevelScheme{
                        RequiredParentsSids = new[]{ "csharp-foundations" },
                        MasterIncrenemt = 1f,
                        MasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                        RequiredJobs = new []{
                            new JobScheme{
                                MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                                CompleteSubTasksAmount = 10
                            }
                        }
                    }
                }
            },
            new SkillScheme{
                Sid = "javascript-foundations",
                Display = "JavaScript Foundations",
                Levels = new[]{
                    new SkillLevelScheme{
                        MasterIncrenemt = 1f,
                        MasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                        RequiredJobs = new []{
                            new JobScheme{
                                MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                                CompleteSubTasksAmount = 10
                            }
                        }
                    }
                }
            },
            new SkillScheme{
                Sid = "angular-foundations",
                Display = "Angular Foundations",
                Levels = new[]{
                    new SkillLevelScheme{
                        MasterIncrenemt = 1f,
                        MasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                        RequiredJobs = new []{
                            new JobScheme{
                                MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                                CompleteSubTasksAmount = 10
                            }
                        }
                    }
                }
            }
        };
    }
}
