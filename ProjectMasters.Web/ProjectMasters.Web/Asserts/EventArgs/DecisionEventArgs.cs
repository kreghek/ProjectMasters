namespace ProjectMasters.Games
{
    using System;

    using Assets.BL;

    public class DecisionEventArgs : EventArgs
    {
        public DecisionEventArgs(Decision decision)
        {
            Decision = decision ?? throw new ArgumentNullException(nameof(decision));
        }

        public Decision Decision { get; }
    }
}