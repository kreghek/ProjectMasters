namespace Assets.BL
{
    public class SkillScheme
    { 
        public string Sid { get; set; }
        public string DisplayTitle { get; internal set; }

        public string[] RequiredParentsSids { get; set; }
        public JobScheme[] RequiredJobs { get; set; }
        public float MasteryIncrenemt { get; set; }
        public MasteryScheme TargetMasteryScheme { get; set; }
    }
}
