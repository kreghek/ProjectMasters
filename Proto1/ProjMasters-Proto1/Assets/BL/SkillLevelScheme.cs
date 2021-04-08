namespace Assets.BL
{
    public class SkillLevelScheme
    {
        public string[] RequiredParentsSids { get; set; }
        public JobScheme[] RequiredJobs { get; set; }
        public float MasterIncrenemt { get; set; }
        public MasteryScheme MasteryScheme { get; set; }
    }
}
