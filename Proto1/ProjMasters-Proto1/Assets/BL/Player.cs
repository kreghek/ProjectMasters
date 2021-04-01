namespace Assets.BL
{
    public static class Player
    {
        public const int DAY_COUNTER_BASE = 24;

        public static int Money { get; set; } = 1000;
        public static int Autority { get; set; } = 2;
        public static int FailureCount { get; set; }

        public static int DayNumber { get; set; }
        public static float DayCounter { get; set; } = DAY_COUNTER_BASE;

        public static Decision WaitForDecision { get; set; }

        public static float DecisionCounter { get; set; } 4;
    }
}
