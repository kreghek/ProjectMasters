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
                Sid = Sids.CSharpFoundations + "1",
                DisplayTitle = "C# Foundations 1",

                MasteryIncrenemt = 1f,
                TargetMasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                RequiredJobs = new []{
                    new JobScheme{
                        MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                        CompleteSubTasksAmount = 10
                    }
                }
            },

            new SkillScheme{
                Sid = Sids.CSharpFoundations + "2",
                DisplayTitle = "C# Foundations 2",

                RequiredParentsSids = new[]{ Sids.CSharpFoundations + "1" },
                MasteryIncrenemt = 0.5f,
                TargetMasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                RequiredJobs = new []{
                    new JobScheme{
                        MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                        CompleteSubTasksAmount = 20
                    }
                }
            },

            new SkillScheme{
                Sid = Sids.CSharpFoundations + "3",
                DisplayTitle = "C# Foundations 3",

                RequiredParentsSids = new[]{ Sids.CSharpFoundations + "2" },
                MasteryIncrenemt = 0.5f,
                TargetMasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                RequiredJobs = new []{
                    new JobScheme{
                        MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                        CompleteSubTasksAmount = 40
                    }
                }
            },

            new SkillScheme{
                Sid = Sids.DotnetAsyncProgramming + "1",
                DisplayTitle = ".NET Async Programming 1",

                RequiredParentsSids = new[]{ Sids.CSharpFoundations + "1" },
                MasteryIncrenemt = 1f,
                TargetMasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                RequiredJobs = new []{
                    new JobScheme{
                        MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                        CompleteSubTasksAmount = 10
                    }
                }
            },

            new SkillScheme{
                Sid = Sids.DotnetAsyncProgramming + "2",
                DisplayTitle = ".NET Async Programming 2",

                RequiredParentsSids = new[]{ Sids.DotnetAsyncProgramming + "1" },
                MasteryIncrenemt = 0.5f,
                TargetMasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                RequiredJobs = new []{
                    new JobScheme{
                        MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                        CompleteSubTasksAmount = 20
                    }
                }
            },

            new SkillScheme{
                Sid = Sids.DotnetAsyncProgramming + "3",
                DisplayTitle = ".NET Async Programming 3",

                RequiredParentsSids = new[]{ Sids.DotnetAsyncProgramming + "2" },
                MasteryIncrenemt = 0.5f,
                TargetMasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                RequiredJobs = new []{
                    new JobScheme{
                        MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                        CompleteSubTasksAmount = 40
                    }
                }
            },

            new SkillScheme{
                Sid = Sids.AspNetCoreFoundations,
                DisplayTitle = "ASP.NET Core Foundations",
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
                DisplayTitle = "JavaScript Foundations",
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
                DisplayTitle = "JS Reactive Programming",
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
                DisplayTitle = "Angular Foundations",
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
