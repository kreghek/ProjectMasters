using System;

namespace Assets.BL
{
    public abstract class ProjectUnitBase
    {
        public abstract ProjectUnitType Type { get; }
        public float Cost { get; set; }
        public float TimeLog { get; set; }
        public SkillScheme[] RequiredSkills { get; set; }

        public abstract void ProcessCommit();

        public bool IsDead { get; set; }

        public int LineIndex { get; set; }

        public int QueueIndex { get; set; }

        public event EventHandler<EventArgs> TakeDamage;

        protected void DoTakeDamage()
        {
            TakeDamage?.Invoke(this, EventArgs.Empty);
        }
    }
}
