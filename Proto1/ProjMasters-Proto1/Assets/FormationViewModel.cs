using Assets.BL;

using UnityEngine;

public class FormationViewModel : MonoBehaviour
{
    public ProjectUnitFormation Formation;

    public ProjectUnitViewModel ProjectUnitViewModelPrefab;

    void Start()
    {
        for (var i = 0; i < 10; i++)
        {
            for (var j = 0; j < 10; j++)
            {
                var unit = Formation.Matrix[i, j];
                if (unit != null)
                {
                    var unitViewModel = Instantiate(ProjectUnitViewModelPrefab);
                    unitViewModel.ProjectUnit = unit;
                    unitViewModel.gameObject.transform.position = new Vector3(i, j);
                }
            }
        }
    }
}
