using Assets.BL;

using UnityEngine;
using UnityEngine.UI;

public class ResourcesPanelHandler : MonoBehaviour
{
    public Text MoneyLabel;
    public Text AuthorityLabel;
    public Text FailuresLabel;

    public GameObject[] FailureUiElements;

    public void Update()
    {
        MoneyLabel.text = Player.Money.ToString();
        AuthorityLabel.text = Player.Autority.ToString();

        foreach (var element in FailureUiElements)
        {
            element.gameObject.SetActive(Player.FailureCount > 0);
        }

        FailuresLabel.text = Player.FailureCount.ToString();
    }
}
