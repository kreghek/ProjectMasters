using Assets.BL;

using UnityEngine;

public class PersonViewModel : MonoBehaviour
{
    public Person Person;

    public void Update()
    {
        Person.Update(Time.deltaTime);
    }
}
