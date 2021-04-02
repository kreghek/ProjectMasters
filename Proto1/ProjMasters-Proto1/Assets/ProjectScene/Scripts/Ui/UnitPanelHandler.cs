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
            var requiredSkills = Player.SelectedUnit.RequiredSkills;
            var skillsDescription = string.Join("\n", requiredSkills.Select(x => $"{x.Sid}"));
            UnitDescriptionText.text = skillsDescription;
        }
        else
        {
            UnitDescriptionText.text = null;
        }
    }
}
