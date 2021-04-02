using Assets.BL;

using UnityEngine;
using UnityEngine.UI;

public class DecisionHandler : MonoBehaviour
{
    public Text DescriptionText;
    public Text Aftermath1Text;
    public Text Aftermath1Description;
    public Text Aftermath2Text;
    public Text Aftermath2Description;

    private Decision _decision;

    public void Init(Decision decision)
    {
        _decision = decision;

        DescriptionText.text = _decision.Text;
        
        Aftermath1Text.text = _decision.Choises[0].Text;
        Aftermath1Description.text = _decision.Choises[0].Description;

        Aftermath2Text.text = _decision.Choises[1].Text;
        Aftermath2Description.text = _decision.Choises[1].Description;

        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Aftermath1_Handler()
    {
        ProcessSelection(_decision.Choises[0]);
    }

    public void Aftermath2_Handler()
    {
        ProcessSelection(_decision.Choises[1]);
    }

    private void ProcessSelection(DecisionAftermathBase decisionAftermath)
    {
        decisionAftermath.Apply();
        Player.WaitForDecision = null;
        Close();
    }
}
