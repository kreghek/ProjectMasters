using Assets.BL;

using UnityEngine;

public class ProjectFactory : MonoBehaviour
{
    public FormationViewModel FormationViewModelPrefab;
    public SpeechWaiter SpeechWaiter;

    private void Start()
    {
        var formation = ProjectUnitFormation.Instance;
        var viewModel = Instantiate(FormationViewModelPrefab);
        viewModel.Formation = formation;

        SpeechWaiter.FormationViewModel = viewModel;
    }
}
