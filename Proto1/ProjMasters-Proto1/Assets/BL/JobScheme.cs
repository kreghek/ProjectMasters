namespace Assets.BL
{
    public class JobScheme
    { 
        public MasteryScheme MasteryScheme { get; set; }
        public int CompleteErrorsAmount { get; set; }
        public int CompleteSubTasksAmount { get; set; }
        public int FeatureDecomposesAmount { get; set; }
    }
}
