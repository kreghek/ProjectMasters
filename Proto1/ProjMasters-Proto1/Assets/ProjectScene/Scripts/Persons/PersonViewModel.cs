using System.Linq;

using Assets.BL;

using UnityEngine;

public class PersonViewModel : MonoBehaviour
{
    public Person Person;

    public GameObject Graphics;
    public SpriteRenderer EyeSpriteRenderer;

    private float? _commitCounter;

    public void Start()
    {
        Person.Commited += Person_Commited;

        EyeSpriteRenderer.sprite = Resources.Load<Sprite>($"Persons/eye{Person.EyeIndex + 1}");
    }

    private void Person_Commited(object sender, System.EventArgs e)
    {
        _commitCounter = 0.5f;
    }

    public void Update()
    {
        if (Person.RecoveryCounter != null)
        {
            // The person is relaxing at home.
            gameObject.transform.position = Vector3.zero;
            return;
        }

        var personLine = ProjectUnitFormation.Instance.Lines.SingleOrDefault(x => x.AssignedPersons.Contains(Person));
        if (personLine == null)
        {
            var formation = ProjectUnitFormation.Instance;

            gameObject.transform.position = Vector3.zero;
        }
        else
        {
            var firstUnit = personLine.Units.FirstOrDefault();
            if (firstUnit != null)
            {
                gameObject.transform.position = new Vector3(firstUnit.QueueIndex - 1, firstUnit.LineIndex);

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
                gameObject.transform.position = Vector3.zero;
            }
        }
    }
}
