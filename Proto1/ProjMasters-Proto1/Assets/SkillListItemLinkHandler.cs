using UnityEngine;
using UnityEngine.UI;

public class SkillListItemLinkHandler : MonoBehaviour
{
    public Text TitleText;
    public string Url { get; set; }

    public void OnClick()
    {
        Application.OpenURL(Url);
    }
}
