using System.Linq;

using UnityEngine;

namespace Assets.BL
{
    public sealed class AddFeatureDecisionAftermath : DecisionAftermathBase
    {
        public override string Description => "New random feature will add to project in random line.";

        public override void Apply()
        {
            var lines = ProjectUnitFormation.Instance.Lines;
            var rollNewFeatureLineIndex = Random.Range(0, lines.Count);

            var randomizedSkills = MasterySchemeCatalog.SkillSchemes.OrderBy(x1 => Random.Range(1, 100));
            var requiredSkillCount = Random.Range(1, MasterySchemeCatalog.SkillSchemes.Length);
            var requiredSkills = randomizedSkills.Take(requiredSkillCount);

            var featureUnit = new FeatureUnit
            {
                Cost = Random.Range(1, 5),
                LineIndex = rollNewFeatureLineIndex,
                RequiredSkills = requiredSkills.ToArray()
            };

            ProjectUnitFormation.Instance.AddUnitIntoLine(rollNewFeatureLineIndex, lines.Count, featureUnit);
        }
    }
}
