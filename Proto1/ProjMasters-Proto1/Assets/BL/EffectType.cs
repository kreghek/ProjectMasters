namespace Assets.BL
{
    public enum EffectType
    { 
        /// <summary>
        /// Decrease commit speed twice.
        /// </summary>
        Procrastination,

        /// <summary>
        /// Increase commit speed twice.
        /// </summary>
        Stream,

        /// <summary>
        /// Increase chance of error.
        /// </summary>
        Scattered,

        /// <summary>
        /// Speed up learning twice.
        /// </summary>
        Evrika,

        /// <summary>
        /// Really reduces a person commit speed.
        /// </summary>
        Despondency = 1000,

        /// <summary>
        /// Really reduces a person commit speed.
        /// </summary>
        Toxic = 1001,

        /// <summary>
        /// Increase damage.
        /// </summary>
        Devastator
    }
}
