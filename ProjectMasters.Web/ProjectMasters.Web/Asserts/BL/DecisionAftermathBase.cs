namespace Assets.BL
{
    public abstract class DecisionAftermathBase
    {
        public string Text { get; set; }

        public abstract void Apply();

        public abstract string Description { get; }
    }
}
