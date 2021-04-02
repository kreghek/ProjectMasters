using Assets.BL;

using UnityEngine;

public class DecisionWaiter : MonoBehaviour
{
    public DecisionHandler DecisionHandler;

    public void Update()
    {
        if (Player.WaitForDecision != null)
        {
            if (!DecisionHandler.gameObject.activeSelf)
            {
                DecisionHandler.Init(Player.WaitForDecision);
            }
        }
    }
}
