using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Assets.BL;

using UnityEngine;
using UnityEngine.UI;

public class ProjectInfoHandler : MonoBehaviour
{
    public Text ProgressText;

    void Update()
    {
        var sumCost = ProjectUnitFormation.Instance.Lines.SelectMany(x => x.Units).Sum(x => x.Cost);
        var sumLog = ProjectUnitFormation.Instance.Lines.SelectMany(x => x.Units).Sum(x => x.TimeLog);
        var percentage = sumLog / sumCost * 100;
        ProgressText.text = $"{percentage:0.##}%";
    }
}
