using Assets.BL;

using UnityEngine;

public class SpeechViewModel : MonoBehaviour
{
    private const int LIFETIME_SEC = 5;
    private float _lifeTimeSec = LIFETIME_SEC;
    private Transform _targetTransform;

    public TextMesh SpeechText;

    public void Init(Speech speech, GameObject gameObject)
    {
        SpeechText.text = speech.Text;
        _targetTransform = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _targetTransform.position;
        _lifeTimeSec -= Time.deltaTime;
        if (_lifeTimeSec <= 0)
        {
            Destroy(gameObject);
        }
    }
}
