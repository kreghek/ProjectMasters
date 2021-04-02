using Assets.BL;

using UnityEngine;

public class ProjectUnitViewModel : MonoBehaviour
{
    private float? _damageTremorCounter;

    private float _targetVerticalScale = 1;
    private float _targetHorizontalScale = 1;
    private Vector3 _lastScaleVector = new Vector3(1, 1);
    private float? _aliveCounter;
    private float _aliveCounterCoef = 1;

    private Color _color;

    public ProjectUnitBase ProjectUnit;

    public SpriteRenderer SpriteRenderer;

    public GameObject Graphics;

    // Start is called before the first frame update
    void Start()
    {
        _color = CalcMainColor();

        SpriteRenderer.color = _color;

        ProjectUnit.TakeDamage += ProjectUnit_TakeDamage;
    }

    private Color CalcMainColor()
    {
        switch (ProjectUnit.Type)
        {
            case ProjectUnitType.Feature:
                return Color.yellow;

            case ProjectUnitType.SubTask:
                return Color.white;

            case ProjectUnitType.Error:
                return Color.red;
        }

        return Color.white;
    }

    public void Update()
    {
        transform.position = new Vector3(ProjectUnit.QueueIndex, ProjectUnit.LineIndex);

        if (_damageTremorCounter != null)
        {
            _damageTremorCounter -= Time.deltaTime;

            var q = Mathf.Sin(_damageTremorCounter.Value * 50);
            Graphics.gameObject.transform.localPosition = new Vector3(q * 0.05f, 0);
            SpriteRenderer.color = Color.Lerp(_color, new Color(1, 1, 1, 0), q);

            if (_damageTremorCounter <= 0)
            {
                _damageTremorCounter = null;
                Graphics.gameObject.transform.localPosition = new Vector3(0, 0);
            }
        }
        else
        {
            SpriteRenderer.color = _color;
        }

        if (_aliveCounter == null)
        {
            _aliveCounter = 1;
            _aliveCounterCoef = Random.Range(0.5f, 1.25f);
            _targetVerticalScale = Random.Range(0.80f, 1.15f);
            _targetHorizontalScale = Random.Range(0.80f, 1.15f);
        }
        else
        {
            _aliveCounter -= Time.deltaTime * _aliveCounterCoef;
            Graphics.transform.localScale = Vector3.Lerp(_lastScaleVector, new Vector3(_targetHorizontalScale, _targetVerticalScale), 1 - _aliveCounter.Value);

            if (_aliveCounter <= 0)
            {
                _aliveCounter = null;
                _lastScaleVector = Graphics.transform.localScale;
            }
        }

    }

    public void OnDestroy()
    {
        ProjectUnit.TakeDamage -= ProjectUnit_TakeDamage;
    }

    private void ProjectUnit_TakeDamage(object sender, System.EventArgs e)
    {
        _damageTremorCounter = 0.5f;
    }

    public void OnMouseEnter()
    {
        Player.SelectedUnit = ProjectUnit;
    }

    public void OnMouseExit()
    {
        Player.SelectedUnit = null;
    }
}
