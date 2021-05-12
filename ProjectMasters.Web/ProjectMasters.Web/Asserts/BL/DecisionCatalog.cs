namespace Assets.BL
{
    public static class DecisionCatalog
    {
        public static Decision[] Decisions { get; } = new[]{
            new Decision
            {
                Text = "You've been afraid the project is late.",
                Choises = new DecisionAftermathBase[]{
                    new PaymentDecisionAftermath{
                        AuthorityCost = -25,
                        Text = "Work at weekend"
                    },
                    new PaymentDecisionAftermath{
                        MoneyCost = -100,
                        Text = "Offer to a outsourcers"
                    }
                }
            },

            new Decision
            {
                Text = "Some of your employees told bad about you",
                Choises = new DecisionAftermathBase[]{
                    new PaymentDecisionAftermath{
                        AuthorityCost = -25,
                        Text = "Ignore"
                    },
                    new AddDismoraleEffectToOneDecisionAftermath{
                        Text = "Find and execute the invader"
                    }
                }
            },

            new Decision
            {
                Text = "The customer ask to add a new little feature.",
                Choises = new DecisionAftermathBase[]{
                    new PaymentDecisionAftermath{
                        AuthorityCost = -25,
                        Text = "Ignore"
                    },
                    new AddFeatureDecisionAftermath
                    {
                        Text = "Plan the new feature"
                    }
                }
            }
        };
    }
}
