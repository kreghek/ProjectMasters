﻿using System.Linq;

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
                Sid = Sids.CSharpFoundations + "-1",
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
                Sid = Sids.CSharpFoundations + "-2",
                DisplayTitle = "C# Foundations 2",

                RequiredParentsSids = new[]{ Sids.CSharpFoundations + "-1" },
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
                Sid = Sids.CSharpFoundations + "-3",
                DisplayTitle = "C# Foundations 3",

                RequiredParentsSids = new[]{ Sids.CSharpFoundations + "-2" },
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
                Sid = Sids.DotnetAsyncProgramming + "-1",
                DisplayTitle = ".NET Async Programming 1",

                RequiredParentsSids = new[]{ Sids.CSharpFoundations + "-1" },
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
                Sid = Sids.DotnetAsyncProgramming + "-2",
                DisplayTitle = ".NET Async Programming 2",

                RequiredParentsSids = new[]{ Sids.DotnetAsyncProgramming + "-1" },
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
                Sid = Sids.DotnetAsyncProgramming + "-3",
                DisplayTitle = ".NET Async Programming 3",

                RequiredParentsSids = new[]{ Sids.DotnetAsyncProgramming + "-2" },
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
                Sid = Sids.AspNetCoreFoundations + "-1",
                DisplayTitle = "ASP.NET Core Foundations",

                RequiredParentsSids = new[]{ Sids.CSharpFoundations + "-1" },
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
                Sid = Sids.AspNetCoreFoundations + "-2",
                DisplayTitle = "ASP.NET Core Foundations",

                RequiredParentsSids = new[]{ Sids.AspNetCoreFoundations + "-1" },
                MasteryIncrenemt = 1f,
                TargetMasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                RequiredJobs = new []{
                    new JobScheme{
                        MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                        CompleteSubTasksAmount = 20
                    }
                }
            },

            new SkillScheme{
                Sid = Sids.AspNetCoreFoundations + "-3",
                DisplayTitle = "ASP.NET Core Foundations",

                RequiredParentsSids = new[]{ Sids.AspNetCoreFoundations + "-2" },
                MasteryIncrenemt = 1f,
                TargetMasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                RequiredJobs = new []{
                    new JobScheme{
                        MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                        CompleteSubTasksAmount = 40
                    }
                }
            },

            new SkillScheme{
                Sid = Sids.AspNetCoreFoundations + "-4",
                DisplayTitle = "ASP.NET Core Foundations",

                RequiredParentsSids = new[]{ Sids.AspNetCoreFoundations + "-3" },
                MasteryIncrenemt = 1f,
                TargetMasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                RequiredJobs = new []{
                    new JobScheme{
                        MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.BackendMastery),
                        CompleteSubTasksAmount = 100
                    }
                }
            },




            new SkillScheme{
                Sid = Sids.JavaScriptFoundations + "-1",
                DisplayTitle = "JavaScript Foundations 1",

                MasteryIncrenemt = 1f,
                TargetMasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                RequiredJobs = new []{
                    new JobScheme{
                        MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                        CompleteSubTasksAmount = 10
                    }
                }
            },

            new SkillScheme{
                Sid = Sids.JavaScriptFoundations + "-2",
                DisplayTitle = "JavaScript Foundations 2",

                RequiredParentsSids = new[]{ Sids.JavaScriptFoundations + "-1" },
                MasteryIncrenemt = 0.5f,
                TargetMasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                RequiredJobs = new []{
                    new JobScheme{
                        MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                        CompleteSubTasksAmount = 20
                    }
                }
            },

            new SkillScheme{
                Sid = Sids.JavaScriptFoundations + "-3",
                DisplayTitle = "JavaScript Foundations 3",

                RequiredParentsSids = new[]{ Sids.JavaScriptFoundations + "-2" },
                MasteryIncrenemt = 0.5f,
                TargetMasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                RequiredJobs = new []{
                    new JobScheme{
                        MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                        CompleteSubTasksAmount = 40
                    }
                }
            },

            new SkillScheme{
                Sid = Sids.JavaScriptReactiveProgramming + "-1",
                DisplayTitle = "JavaScript Reactive Programming 1",

                RequiredParentsSids = new[]{ Sids.JavaScriptFoundations + "-1" },
                MasteryIncrenemt = 1f,
                TargetMasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                RequiredJobs = new []{
                    new JobScheme{
                        MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                        CompleteSubTasksAmount = 10
                    }
                }
            },

            new SkillScheme{
                Sid = Sids.JavaScriptReactiveProgramming + "-2",
                DisplayTitle = "JavaScript Reactive Programming 2",

                RequiredParentsSids = new[]{ Sids.JavaScriptReactiveProgramming + "-1" },
                MasteryIncrenemt = 0.5f,
                TargetMasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                RequiredJobs = new []{
                    new JobScheme{
                        MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                        CompleteSubTasksAmount = 20
                    }
                }
            },

            new SkillScheme{
                Sid = Sids.JavaScriptReactiveProgramming + "-3",
                DisplayTitle = "JavaScript Reactive Programming 3",

                RequiredParentsSids = new[]{ Sids.JavaScriptReactiveProgramming + "-2" },
                MasteryIncrenemt = 0.5f,
                TargetMasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                RequiredJobs = new []{
                    new JobScheme{
                        MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                        CompleteSubTasksAmount = 40
                    }
                }
            },

            new SkillScheme{
                Sid = Sids.AngularFoundations + "-1",
                DisplayTitle = "Angular Foundations",

                RequiredParentsSids = new[]{ Sids.JavaScriptFoundations + "-1" },
                MasteryIncrenemt = 1f,
                TargetMasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                RequiredJobs = new []{
                    new JobScheme{
                        MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                        CompleteSubTasksAmount = 10
                    }
                }
            },

            new SkillScheme{
                Sid = Sids.AngularFoundations + "-2",
                DisplayTitle = "Angular Foundations",

                RequiredParentsSids = new[]{ Sids.AngularFoundations + "-1" },
                MasteryIncrenemt = 1f,
                TargetMasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                RequiredJobs = new []{
                    new JobScheme{
                        MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                        CompleteSubTasksAmount = 20
                    }
                }
            },

            new SkillScheme{
                Sid = Sids.AngularFoundations + "-3",
                DisplayTitle = "Angular Foundations",

                RequiredParentsSids = new[]{ Sids.AngularFoundations + "-2" },
                MasteryIncrenemt = 1f,
                TargetMasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                RequiredJobs = new []{
                    new JobScheme{
                        MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                        CompleteSubTasksAmount = 40
                    }
                }
            },

            new SkillScheme{
                Sid = Sids.AngularFoundations + "-4",
                DisplayTitle = "Angular Foundations",

                RequiredParentsSids = new[]{ Sids.AngularFoundations + "-3" },
                MasteryIncrenemt = 1f,
                TargetMasteryScheme = MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                RequiredJobs = new []{
                    new JobScheme{
                        MasteryScheme =  MasterySchemeCatalog.SkillSchemes.Single(x=>x.Sid == MasterySchemeCatalog.Sids.FrontendMastery),
                        CompleteSubTasksAmount = 100
                    }
                }
            },

        };
    }
}
