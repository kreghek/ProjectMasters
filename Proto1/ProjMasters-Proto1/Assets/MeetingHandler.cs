using System.Collections;
using System.Collections.Generic;

using Assets.BL;

using UnityEngine;
using UnityEngine.UI;

public class MeetingHandler : MonoBehaviour
{
    private MeetingDialogNode _currentNode;

    public MeetingAnswerHandler MeetingAnswerHandlerPrefab;
    public Text NodeText;
    public Transform AnswersParent;

    public void Start()
    {
        _currentNode = Player.MeetingNode;
        RefillDialogNode(Player.MeetingNode);
    }

    private void RefillDialogNode(MeetingDialogNode dialogNode)
    {
        foreach (Transform child in AnswersParent)
        {
            Destroy(child.gameObject);
        }

        NodeText.text = $"<b>{dialogNode.SpeakerName}</b>: {dialogNode.Text}";

        if (dialogNode.Answers != null)
        {
            foreach (var answer in dialogNode.Answers)
            {
                var answerViewModel = Instantiate(MeetingAnswerHandlerPrefab, AnswersParent);
                answerViewModel.Answer = answer;
                answerViewModel.Selected += AnswerViewModel_Selected;
            }
        }

        if (dialogNode.IsEndNode)
        {
            var answerViewModel = Instantiate(MeetingAnswerHandlerPrefab, AnswersParent);
            answerViewModel.IsEndNode = true;
        }
    }

    private void AnswerViewModel_Selected(object sender, System.EventArgs e)
    {
        if (Player.MeetingNode != null)
        {
            RefillDialogNode(Player.MeetingNode);
        }

        if (Player.MeetingNode.IsEndNode)
        {
            Player.Money += ProjectUnitFormation.Instance.ProjectMoneyEarning;
            Player.Autority += ProjectUnitFormation.Instance.ProjectAuthrityEarning;

            ProjectUnitFormation.Instance.Recreate();
        }
    }
}
