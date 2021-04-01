namespace Assets.BL
{
    public class Effect
    {
        public const float MIN_DURATION = 2;
        public const float MAX_DURATION = 16;

        public EffectType EffectType { get; set; }
        public float Lifetime { get; set; }
    }
}
