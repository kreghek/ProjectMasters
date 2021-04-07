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

    public void ChangeLine(int lineIndex)
    {
        var targetLine = ProjectUnitFormation.Instance.Lines[lineIndex];

        var currentPersonLine = ProjectUnitFormation.Instance.Lines.SingleOrDefault(x => x.AssignedPersons.Contains(Person));

        if (currentPersonLine != null)
        {
            currentPersonLine.AssignedPersons.Remove(Person);
        }

        targetLine.AssignedPersons.Add(Person);
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

        StatsText.text = sb.ToString();

        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
