using System.Linq;
using System.Text;

using Assets.BL;

using UnityEngine;
using UnityEngine.UI;

public class PersonCardModalHandler : MonoBehaviour
{
    public Person Person;
    public PersonAvatarHandler PersonAvatarHandler;
    public Text StatsText;
    public SkillListItem SkillListItemPrefab;
    public Transform SkillsParent;
    public Text DescriptionText;

    public void ChangeLine(int lineIndex)
    {
        var targetLine = ProjectUnitFormation.Instance.Lines[lineIndex];

        var currentPersonLine = ProjectUnitFormation.Instance.Lines.SingleOrDefault(x => x.AssignedPersons.Contains(Person));

        if (currentPersonLine != null)
        {
            currentPersonLine.AssignedPersons.Remove(Person);
        }

        targetLine.AssignedPersons.Add(Person);
        Person.SetChangeLineCounter();
    }

    public void Show(Person person)
    {
        Person = person;

        PersonAvatarHandler.Person = Person;

        var sb = new StringBuilder();

        sb.AppendLine($"Feature decomposes: {Person.FeatureCompleteCount}");
        sb.AppendLine($"Sub tasks: {Person.SubTasksCompleteCount}");
        sb.AppendLine($"Error Fixes: {Person.ErrorCompleteCount}");
        sb.AppendLine($"Error Made: {Person.ErrorMadeCount}");
        /*sb.AppendLine(string.Join(", ", Person.Skills.Select(x => x.Scheme.DisplayTitle)));

        if (Person.ActiveSkill != null)
        {
            sb.AppendLine($"Active skill: {Person.ActiveSkill.Scheme.DisplayTitle} ({Person.ActiveSkill.GetPercentage()}%)");
        }*/

        StatsText.text = sb.ToString();

        foreach (Transform childItem in SkillsParent)
        {
            Destroy(childItem.gameObject);
        }

        foreach (var skill in Person.Skills)
        {
            var skillItem = Instantiate(SkillListItemPrefab, SkillsParent);
            skillItem.Skill = skill;
            skillItem.DescriptionText = DescriptionText;
        }

        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
