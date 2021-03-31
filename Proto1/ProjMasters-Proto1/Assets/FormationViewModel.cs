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
                    var unitViewModel = Instantiate(ProjectUnitViewModelPrefab, transform);
                    unitViewModel.ProjectUnit = unit;
                    unitViewModel.gameObject.transform.position = new Vector3(i, j);
                }
            }
        }

        Formation.Added += Formation_Added;
        Formation.Removed += Formation_Removed;
    }

    private void Formation_Removed(object sender, UnitEventArgs e)
    {
        var unitViewModels = GetComponentsInChildren<ProjectUnitViewModel>();
        foreach (var unitViewModel in unitViewModels)
        {
            if (unitViewModel.ProjectUnit == e.ProjectUnit)
            {
                Destroy(unitViewModel.gameObject);
            }
        }
    }

    private void Formation_Added(object sender, UnitEventArgs e)
    {
        var unitViewModel = Instantiate(ProjectUnitViewModelPrefab);
        unitViewModel.ProjectUnit = e.ProjectUnit;
        unitViewModel.gameObject.transform.position = new Vector3(e.X, e.Y);
    }
}
