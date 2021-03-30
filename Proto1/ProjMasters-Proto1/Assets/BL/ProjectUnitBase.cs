namespace Assets.BL
{
    public abstract class ProjectUnitBase
    {
        public abstract ProjectUnitType Type { get; }
        public float Cost { get; set; }
        public float TimeLog { get; set; }
        public SkillScheme[] RequiredSkills { get; set; }

        public abstract void ProcessCommit();
    }
}
