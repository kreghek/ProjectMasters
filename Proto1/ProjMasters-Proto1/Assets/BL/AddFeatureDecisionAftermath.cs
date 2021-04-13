using System.Linq;

using UnityEngine;

namespace Assets.BL
{
    public sealed class AddFeatureDecisionAftermath : DecisionAftermathBase
    {
        public override string Description => "New random feature will add to the project in random line.";

        public override void Apply()
        {
            var lines = ProjectUnitFormation.Instance.Lines;
            var rollNewFeatureLineIndex = Random.Range(0, lines.Count);

            var randomizedMasteries = new[] { SkillCatalog.MasterySids.BackendMastery, SkillCatalog.MasterySids.FrontendMastery }.OrderBy(x1 => Random.Range(1, 100));
            var requiredMasteryCount = Random.Range(1, randomizedMasteries.Count() + 1);
            var requiredMasteries = randomizedMasteries.Take(requiredMasteryCount);

            var featureUnit = new FeatureUnit
            {
                Cost = Random.Range(1, 5),
                LineIndex = rollNewFeatureLineIndex,
                RequiredMasteryItems = requiredMasteries.ToArray()
            };

            ProjectUnitFormation.Instance.AddUnitIntoLine(rollNewFeatureLineIndex, lines.Count, featureUnit);
        }
    }
}
