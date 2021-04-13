namespace Assets.BL
{
    public class SkillScheme
    { 
        public string Sid { get; set; }
        public string DisplayTitle { get; internal set; }
        public string Description { get; set; }
        public string KnowedgeBaseUrl { get; set; }

        public string[] RequiredParentsSids { get; set; }
        public JobScheme[] RequiredJobs { get; set; }
        public float MasteryIncrenemt { get; set; }
        public string[] MasteryTags { get; set; }
    }
}
