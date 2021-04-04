using Assets.BL;

using UnityEngine;

public class PersonsPanelHandler : MonoBehaviour
{
    public PersonCardHandler PersonCardHandlerPrefab;

    public PersonCardModalHandler PersonCardModalHandler;

    public void Init()
    {
        foreach (var person in Team.Persons)
        {
            var card = Instantiate(PersonCardHandlerPrefab, transform);
            card.Person = person;
            card.PersonModalClicked += Card_PersonModalClicked;
        }
    }

    private void Card_PersonModalClicked(object sender, System.EventArgs e)
    {
        PersonCardModalHandler.Show(((PersonCardHandler)sender).Person);
    }
}
