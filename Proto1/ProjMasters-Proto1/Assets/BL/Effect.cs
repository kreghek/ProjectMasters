namespace Assets.BL
{
    public class Effect
    {
        public const float MIN_DURATION = 100;
        public const float MAX_DURATION = 240;

        public EffectType EffectType { get; set; }
        public float Lifetime { get; set; }
    }
}
