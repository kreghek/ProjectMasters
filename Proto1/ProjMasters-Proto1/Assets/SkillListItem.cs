﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Assets.BL;

using UnityEngine;
using UnityEngine.UI;

public class SkillListItem : MonoBehaviour
{
    public Skill Skill { get; internal set; }

    public Text TitleText;

    public Text DescriptionText;
    public Button KnowedgeBaseLinkButton;

    public Image StateImage;

    // Start is called before the first frame update
    void Start()
    {
        TitleText.text = Skill.Scheme.DisplayTitle;

    }

    public void Select()
    {
        var sb = new StringBuilder();

        sb.AppendLine(Skill.Scheme.DisplayTitle);
        if (Skill.Scheme.Description != null)
        {
            sb.AppendLine(Skill.Scheme.Description);
        }

        if (Skill.Scheme.KnowedgeBaseUrl != null)
        {
            KnowedgeBaseLinkButton.gameObject.SetActive(true);
        }
        else
        {
            KnowedgeBaseLinkButton.gameObject.SetActive(false);
        }

        if (!Skill.IsLearnt)
        {
            if (Skill.Scheme.RequiredParentsSids != null && Skill.Scheme.RequiredParentsSids.Any())
            {
                sb.AppendLine("Required skills:");
                foreach (var parentSid in Skill.Scheme.RequiredParentsSids)
                {
                    var parentScheme = SkillCatalog.AllSchemes.Single(x=>x.Sid == parentSid);
                    sb.AppendLine($"- {parentScheme.DisplayTitle}");
                }
            }

            sb.AppendLine("Progress:");
            foreach (var requiredJobScheme in Skill.Scheme.RequiredJobs)
            {
                var progress = 0;

                var currentJob = Skill.Jobs.SingleOrDefault(x => x.Scheme == requiredJobScheme);
                if (currentJob != null)
                {
                    progress = currentJob.CompleteSubTasksAmount;
                }

                sb.AppendLine($"- Complete sub tasks: {progress}/{requiredJobScheme.CompleteSubTasksAmount}");
            }
        }

        DescriptionText.text = sb.ToString();
    }
}
