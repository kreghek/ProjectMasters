using Assets.BL;

using UnityEngine;

public class PersonViewModel : MonoBehaviour
{
    public Person Person;

    public GameObject Graphics;

    private float? _commitCounter;

    public void Start()
    {
        Person.Commited += Person_Commited;
    }

    private void Person_Commited(object sender, System.EventArgs e)
    {
        if (Person.Assigned != null)
        {
            _commitCounter = 0.5f;
        }
    }

    public void Update()
    {
        if (Person.Assigned != null)
        {
            gameObject.transform.position = new Vector3(Person.Assigned.X - 1, Person.Assigned.Y);

            if (_commitCounter != null)
            {
                _commitCounter -= Time.deltaTime;

                Graphics.transform.localPosition = new Vector3(Mathf.Sin(_commitCounter.Value / 0.5f) * 0.5f, 0);

                if (_commitCounter <= 0)
                {
                    _commitCounter = 0;
                    Graphics.transform.localPosition = new Vector3(0, 0);
                }
            }
        }
        else
        {
            gameObject.transform.position = new Vector3(0, 0);
        }
    }
}
