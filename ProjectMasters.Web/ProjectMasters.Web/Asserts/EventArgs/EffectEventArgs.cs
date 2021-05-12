namespace ProjectMasters.Games
{
    using System;

    using Assets.BL;

    public class EffectEventArgs : EventArgs
    {
        public EffectEventArgs(Effect effect)
        {
            Effect = effect ?? throw new ArgumentNullException(nameof(effect));
        }

        public Effect Effect { get; }
    }
}