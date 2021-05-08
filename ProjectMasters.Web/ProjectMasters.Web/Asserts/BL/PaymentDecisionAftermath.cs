namespace Assets.BL
{
    public sealed class PaymentDecisionAftermath : DecisionAftermathBase
    {
        public int MoneyCost { get; set; }

        public int AuthorityCost { get; set; }
        public override string Description => MoneyCost != 0 ? $"{MoneyCost}$" : $"Authority {AuthorityCost}";

        public override void Apply()
        {
            Player.Autority += AuthorityCost;
            Player.Money += MoneyCost;
        }
    }
}
