using System.Linq;

using Assets.BL;

using UnityEngine;

public class SpeechWaiter : MonoBehaviour
{
    public FormationViewModel FormationViewModel;
    public SpeechViewModel SpeechViewModelPrefab;

    void Start()
    {
        SpeechPool.SpeechAdded += SpeechPool_SpeechAdded;
    }

    private void SpeechPool_SpeechAdded(object sender, SpeechAddedEventArgs e)
    {
        var allUnitModels = FormationViewModel.GetComponentsInChildren<ProjectUnitViewModel>();
        var unitViewModel = allUnitModels.SingleOrDefault(x => x.ProjectUnit == e.Speech.Source);
        if (unitViewModel != null)
        {
            var viewModel = Instantiate(SpeechViewModelPrefab, FormationViewModel.transform);
            viewModel.Init(e.Speech, unitViewModel.gameObject);
        }
    }
}