using Assets.BL;

using UnityEngine;

public class PersonsPanelHandler : MonoBehaviour
{
    public PersonCardHandler PersonCardHandlerPrefab;

    public void Init()
    {
        foreach (var person in Team.Persons)
        {
            var card = Instantiate(PersonCardHandlerPrefab, transform);
            card.Person = person;
        }
    }
}
