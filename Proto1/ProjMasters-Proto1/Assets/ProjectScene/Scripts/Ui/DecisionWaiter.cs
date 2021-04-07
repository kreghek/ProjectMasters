using Assets.BL;

using UnityEngine;

public class DecisionWaiter : MonoBehaviour
{
    public DecisionHandler DecisionHandler;

    public void Update()
    {
        if (Player.WaitForDecision != null && !Player.WaitKeyDayReport)
        {
            if (!DecisionHandler.gameObject.activeSelf)
            {
                DecisionHandler.Show(Player.WaitForDecision);
            }
        }
    }
}
