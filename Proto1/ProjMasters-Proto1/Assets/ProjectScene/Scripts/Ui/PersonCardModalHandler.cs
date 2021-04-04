using System.Linq;

using Assets.BL;

using UnityEngine;

public class PersonCardModalHandler : MonoBehaviour
{
    public Person Person;
    public PersonAvatarHandler PersonAvatarHandler;

    // Start is called before the first frame update
    void Start()
    {
        PersonAvatarHandler.Person = Person;
    }

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
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
