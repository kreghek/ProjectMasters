namespace Assets.BL
{
    public static class MasterySchemeCatalog
    {
        public static MasteryScheme[] SkillSchemes = new[] {
            new MasteryScheme{ Sid = Sids.FrontendMastery },
            new MasteryScheme{ Sid = Sids.BackendMastery },
        };

        public static class Sids
        {
            public static string BackendMastery = "back";
            public static string FrontendMastery = "front";
        }
    }
}
