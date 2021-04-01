﻿using System.Linq;

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

    void OldStart()
    {
        var formation = ProjectUnitFormation.Instance;

        var featureCount = Random.Range(3, 5);
        for (int i = 0; i < featureCount; i++)
        {
            var x = Random.Range(3, 7);
            var y = Random.Range(3, 7);

            var feature = new FeatureUnit
            {
                Cost = Random.Range(40, 60)
            };

            var randomizedSkills = SkillSchemeCatalog.SkillSchemes.OrderBy(x1 => Random.Range(1, 100));
            var requiredSkillCount = Random.Range(1, SkillSchemeCatalog.SkillSchemes.Length);
            var requiredSkills = randomizedSkills.Take(requiredSkillCount);
            feature.RequiredSkills = requiredSkills.ToArray();

            //formation.AddUnitInClosestPosition(feature, x, y);
        }

        var viewModel = Instantiate(FormationViewModelPrefab);
        viewModel.Formation = formation;
    }
}