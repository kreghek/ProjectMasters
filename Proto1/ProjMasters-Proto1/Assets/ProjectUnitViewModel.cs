using Assets.BL;

using UnityEngine;

public class ProjectUnitViewModel : MonoBehaviour
{
    public ProjectUnitBase ProjectUnit;

    public SpriteRenderer SpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        switch (ProjectUnit.Type)
        {
            case ProjectUnitType.Feature:
                SpriteRenderer.color = Color.yellow;
                break;

            case ProjectUnitType.SubTask:
                SpriteRenderer.color = Color.white;
                break;

            case ProjectUnitType.Error:
                SpriteRenderer.color = Color.red;
                break;
        }
    }
}
