using System.Linq;
using System.Text;

using Assets.BL;

using UnityEngine;
using UnityEngine.UI;

public class DayReportModalHandler : MonoBehaviour
{
    public Text ProjectStatusText;

    public void Show()
    {
        var sb = new StringBuilder();

        var sumCost = ProjectUnitFormation.Instance.Lines.SelectMany(x => x.Units).Sum(x => x.Cost);
        var sumLog = ProjectUnitFormation.Instance.Lines.SelectMany(x => x.Units).Sum(x => x.TimeLog);

        var solvedSumCost = ProjectUnitFormation.Instance.SolvedUnits.Where(x => x.Day == Player.DayNumber - 1).Sum(x => x.Cost);
        var solvedTimeLogCost = ProjectUnitFormation.Instance.SolvedUnits.Where(x => x.Day == Player.DayNumber - 1).Sum(x => x.TimeLog);

        var solvedSumCost2 = ProjectUnitFormation.Instance.SolvedUnits.Where(x => x.Day == Player.DayNumber - 2).Sum(x => x.Cost);
        var solvedTimeLogCost2 = ProjectUnitFormation.Instance.SolvedUnits.Where(x => x.Day == Player.DayNumber - 2).Sum(x => x.TimeLog);

        sb.AppendLine($"Solved this day: {solvedTimeLogCost:0.##}/{solvedSumCost:0.##}");
        sb.AppendLine($"Solved prev day: {solvedTimeLogCost2:0.##}/{solvedSumCost2:0.##}");
        sb.AppendLine($"Remains: {sumLog:0.##}/{sumCost:0.##}");

        ProjectStatusText.text = sb.ToString();

        gameObject.SetActive(true);
    }

    public void Close()
    {
        Player.WaitKeyDayReport = false;
        gameObject.SetActive(false);
    }
}
