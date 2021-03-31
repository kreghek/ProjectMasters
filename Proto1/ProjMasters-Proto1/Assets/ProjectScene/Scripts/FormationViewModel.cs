using Assets.BL;

using UnityEngine;

public class FormationViewModel : MonoBehaviour
{
    public ProjectUnitFormation Formation;

    public ProjectUnitViewModel ProjectUnitViewModelPrefab;

    void Start()
    {
        for (var lineIndex = 0; lineIndex < Formation.Lines.Count; lineIndex++)
        {
            for (var unitIndex = 0; unitIndex < Formation.Lines[lineIndex].Units.Count; unitIndex++)
            {
                var unit = Formation.Lines[lineIndex].Units[unitIndex];
                var unitViewModel = Instantiate(ProjectUnitViewModelPrefab, transform);
                unitViewModel.ProjectUnit = unit;
            }
        }

        Formation.Added += Formation_Added;
        Formation.Removed += Formation_Removed;
    }

    private void OnDestroy()
    {
        Formation.Added -= Formation_Added;
        Formation.Removed -= Formation_Removed;
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
        var unitViewModel = Instantiate(ProjectUnitViewModelPrefab, transform);
        unitViewModel.ProjectUnit = e.ProjectUnit;
    }
}
