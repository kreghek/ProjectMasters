namespace Assets.BL
{
    public class Job
    {
        public JobScheme Scheme { get; set; }
        public int CompleteErrorsAmount { get; set; }
        public int CompleteSubTasksAmount { get; set; }
        public int FeatureDecomposesAmount { get; set; }
    }
}