namespace Assets.BL
{
    public class Effect
    {
        public const float MAX_DURATION = 240;
        public const float MIN_DURATION = 100;

        public EffectType EffectType { get; set; }
        public int Id { get; set; }
        public float Lifetime { get; set; }
    }
}