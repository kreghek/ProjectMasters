using ProjectMasters.Games;

namespace Assets.BL
{
    public sealed class PaymentDecisionAftermath : DecisionAftermathBase
    {
        public int AuthorityCost { get; set; }
        public override string Description => MoneyCost != 0 ? $"{MoneyCost}$" : $"Authority {AuthorityCost}";
        public int MoneyCost { get; set; }

        public override void Apply(GameState gameState)
        {
            Player.Autority += AuthorityCost;
            Player.Money += MoneyCost;
        }
    }
}