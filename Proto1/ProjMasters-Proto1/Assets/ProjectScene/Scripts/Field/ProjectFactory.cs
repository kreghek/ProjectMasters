using Assets.BL;

using UnityEngine;

public class ProjectFactory : MonoBehaviour
{
    public FormationViewModel FormationViewModelPrefab;

    private void Start()
    {
        var formation = ProjectUnitFormation.Instance;
        var viewModel = Instantiate(FormationViewModelPrefab);
        viewModel.Formation = formation;
    }
}
