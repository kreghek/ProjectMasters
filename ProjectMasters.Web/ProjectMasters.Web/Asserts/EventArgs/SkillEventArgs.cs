namespace ProjectMasters.Games
{
    using System;

    using Assets.BL;

    public class SkillEventArgs : EventArgs
    {
        public SkillEventArgs(Skill skill)
        {
            this.skill = skill ?? throw new ArgumentNullException(nameof(skill));
        }

        public Skill skill { get; }
    }
}