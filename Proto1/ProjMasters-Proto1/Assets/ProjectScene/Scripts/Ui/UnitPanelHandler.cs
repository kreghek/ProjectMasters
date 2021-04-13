using System.Linq;

using Assets.BL;

using UnityEngine;
using UnityEngine.UI;

public class UnitPanelHandler : MonoBehaviour
{
    public Text UnitDescriptionText;

    // Update is called once per frame
    void Update()
    {
        if (Player.SelectedUnit != null)
        {
            var requiredSkills = Player.SelectedUnit.RequiredMasteryItems;
            string skillsDescription;

            if (Player.SelectedUnit is FeatureUnit)
            {
                skillsDescription = string.Empty;
            }
            else
            {
                skillsDescription = string.Join("\n", requiredSkills.Select(x => $"{x}"));
            }

            UnitDescriptionText.text = $"{skillsDescription}\n{Player.SelectedUnit.TimeLog:0.##}/{Player.SelectedUnit.Cost}";
        }
        else
        {
            UnitDescriptionText.text = null;
        }
    }
}
