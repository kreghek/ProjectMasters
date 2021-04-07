using Assets.BL;

using UnityEngine;

public class DayReportWaiter : MonoBehaviour
{
    public DayReportModalHandler DayReportModalHandler;

    public void Update()
    {
        if (Player.WaitForDecision != null && Player.WaitKeyDayReport)
        {
            if (!DayReportModalHandler.gameObject.activeSelf)
            {
                DayReportModalHandler.Show();
            }
        }
    }
}
