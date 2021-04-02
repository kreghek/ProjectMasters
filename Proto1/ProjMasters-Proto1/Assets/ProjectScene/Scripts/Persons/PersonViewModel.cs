using System.Linq;

using Assets.BL;

using UnityEngine;

public class PersonViewModel : MonoBehaviour
{
    public Person Person;

    public GameObject Graphics;
    public SpriteRenderer EyeSpriteRenderer;
    public SpriteRenderer FaceDecorSpriteRenderer;

    private float? _commitCounter;
    private Vector3? _moveTargetPosition;

    public void Start()
    {
        Person.Commited += Person_Commited;

        EyeSpriteRenderer.sprite = Resources.Load<Sprite>($"Persons/eye{Person.EyeIndex + 1}");
        FaceDecorSpriteRenderer.sprite = Resources.Load<Sprite>($"Persons/face-decor{Person.FaceDecorIndex}");

        gameObject.transform.position = GetRestPosition();
    }

    private void Person_Commited(object sender, System.EventArgs e)
    {
        _commitCounter = 0.5f;
    }

    public void Update()
    {
        if (_moveTargetPosition != null)
        {
            if ((gameObject.transform.position - _moveTargetPosition.Value).magnitude > 0.001f)
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, _moveTargetPosition.Value, Time.deltaTime);
            }
        }

        if (Person.RecoveryCounter != null)
        {
            // The person is relaxing at home.
            _moveTargetPosition = GetRestPosition();
            return;
        }

        var personLine = ProjectUnitFormation.Instance.Lines.SingleOrDefault(x => x.AssignedPersons.Contains(Person));
        if (personLine == null)
        {
            var formation = ProjectUnitFormation.Instance;

            _moveTargetPosition = GetRestPosition();
        }
        else
        {
            var firstUnit = personLine.Units.FirstOrDefault();
            if (firstUnit != null)
            {
                _moveTargetPosition = new Vector3(firstUnit.QueueIndex - 1, firstUnit.LineIndex);

                if (_commitCounter != null)
                {
                    _commitCounter -= Time.deltaTime;

                    Graphics.transform.localPosition = new Vector3(Mathf.Sin(_commitCounter.Value / 0.5f) * 0.5f, 0);

                    if (_commitCounter <= 0)
                    {
                        _commitCounter = 0;
                        Graphics.transform.localPosition = Vector3.zero;
                    }
                }
            }
            else
            {
                _moveTargetPosition = GetRestPosition();
            }
        }
    }

    private static Vector3 GetRestPosition()
    {
        var positionOffset = Random.insideUnitCircle * 0.2f;
        return new Vector3(-2 + positionOffset.x, positionOffset.y, 0);
    }
}
