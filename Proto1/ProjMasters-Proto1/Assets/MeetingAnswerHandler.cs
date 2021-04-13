using System;

using Assets.BL;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MeetingAnswerHandler : MonoBehaviour
{
    public MeetingAnswer Answer { get; internal set; }

    public event EventHandler<EventArgs> Selected;

    public Text AnswerText;

    public bool IsEndNode { get; set; }

    public void Start()
    {
        if (!IsEndNode)
        {
            AnswerText.text = Answer.Text;
        }
        else
        {
            AnswerText.text = "[End meeting]";
        }
    }

    public void ButtonClick()
    {
        if (Answer != null)
        {
            Answer.Apply();
            Selected?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            SceneManager.LoadScene("project");
        }
    }
}
