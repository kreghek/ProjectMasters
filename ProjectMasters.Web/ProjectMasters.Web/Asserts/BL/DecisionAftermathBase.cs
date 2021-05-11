namespace Assets.BL
{
    public abstract class DecisionAftermathBase
    {
        public abstract string Description { get; }
        public string Text { get; set; }

        public abstract void Apply();
    }
}