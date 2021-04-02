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

        var solvedSumCost = ProjectUnitFormation.Instance.SolvedUnits.Sum(x => x.Cost);
        var solvedTimeLogCost = ProjectUnitFormation.Instance.SolvedUnits.Sum(x => x.TimeLog);

        var percentage = (sumLog + solvedTimeLogCost) / (sumCost + solvedSumCost) * 100;
        ProgressText.text = $"{percentage:0.##}%";
    }
}
