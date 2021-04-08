using System.Linq;

namespace Assets.BL
{
    public static class SkillCatalog
    {
        public static class Sids
        {
            public static string CSharpFoundations = "csharp-foundations";
            public static string DotnetAsyncProgramming = "dotnet-async-programming";
            public static string AspNetCoreFoundations = "asp-net-core-foundations";
            public static string JavaScriptFoundations = "javascript-foundations";
            public static string JavaScriptReactiveProgramming = "javascript-foundations";
            public static string AngularFoundations = "angular-foundations";
        }

        public static SkillScheme[] AllSchemes { get; } = new SkillScheme[] {
            new SkillScheme{
                Sid = Sids.CSharpFoundations,
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
                    },
                    new SkillLevelScheme{
                        MasterIncrenemt = 0.5f,
                        MasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                        RequiredJobs = new []{
                            new JobScheme{
                                MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                                CompleteSubTasksAmount = 10
                            }
                        }
                    },
                    new SkillLevelScheme{
                        MasterIncrenemt = 0.5f,
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
                Sid = Sids.DotnetAsyncProgramming,
                Display = ".NET Async Programming",
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
                    },
                    new SkillLevelScheme{
                        MasterIncrenemt = 0.5f,
                        MasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                        RequiredJobs = new []{
                            new JobScheme{
                                MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                                CompleteSubTasksAmount = 10
                            }
                        }
                    },
                    new SkillLevelScheme{
                        MasterIncrenemt = 0.5f,
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
                Sid = Sids.AspNetCoreFoundations,
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
                    },
                    new SkillLevelScheme{
                        RequiredParentsSids = new[]{ "csharp-foundations" },
                        MasterIncrenemt = 1f,
                        MasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                        RequiredJobs = new []{
                            new JobScheme{
                                MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                                CompleteSubTasksAmount = 20
                            }
                        }
                    },
                    new SkillLevelScheme{
                        RequiredParentsSids = new[]{ "csharp-foundations" },
                        MasterIncrenemt = 1f,
                        MasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                        RequiredJobs = new []{
                            new JobScheme{
                                MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                                CompleteSubTasksAmount = 40
                            }
                        }
                    },
                    new SkillLevelScheme{
                        RequiredParentsSids = new[]{ "csharp-foundations" },
                        MasterIncrenemt = 1f,
                        MasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                        RequiredJobs = new []{
                            new JobScheme{
                                MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                                CompleteSubTasksAmount = 100
                            }
                        }
                    }
                }
            },
            new SkillScheme{
                Sid = Sids.JavaScriptFoundations,
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
                    },
                    new SkillLevelScheme{
                        MasterIncrenemt = 0.5f,
                        MasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                        RequiredJobs = new []{
                            new JobScheme{
                                MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                                CompleteSubTasksAmount = 10
                            }
                        }
                    },
                    new SkillLevelScheme{
                        MasterIncrenemt = 0.5f,
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
                Sid = Sids.JavaScriptReactiveProgramming,
                Display = "JS Reactive Programming",
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
                    },
                    new SkillLevelScheme{
                        MasterIncrenemt = 0.5f,
                        MasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                        RequiredJobs = new []{
                            new JobScheme{
                                MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                                CompleteSubTasksAmount = 10
                            }
                        }
                    },
                    new SkillLevelScheme{
                        MasterIncrenemt = 0.5f,
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
                Sid = Sids.AngularFoundations,
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
                    },
                    new SkillLevelScheme{
                        MasterIncrenemt = 1f,
                        MasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                        RequiredJobs = new []{
                            new JobScheme{
                                MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                                CompleteSubTasksAmount = 20
                            }
                        }
                    },
                    new SkillLevelScheme{
                        MasterIncrenemt = 1f,
                        MasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                        RequiredJobs = new []{
                            new JobScheme{
                                MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                                CompleteSubTasksAmount = 40
                            }
                        }
                    },
                    new SkillLevelScheme{
                        MasterIncrenemt = 1f,
                        MasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                        RequiredJobs = new []{
                            new JobScheme{
                                MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                                CompleteSubTasksAmount = 100
                            }
                        }
                    }
                }
            }
        };
    }
}
