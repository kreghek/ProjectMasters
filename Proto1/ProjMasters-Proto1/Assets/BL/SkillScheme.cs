namespace Assets.BL
{
    public class SkillScheme
    { 
        public string Sid { get; set; }
        public SkillLevelScheme[] Levels { get; set; }
        public string Display { get; internal set; }
    }
}
