import { Component, OnInit } from '@angular/core';
import { FeaturesService } from '../features.service';
import { Feature } from '../models/project';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-feature-list',
  templateUrl: './feature-list.component.html',
  styleUrls: ['./feature-list.component.css']
})
export class FeatureListComponent implements OnInit {

  public featureList: Feature[];
  private routeSub: Subscription;

  constructor(private featureService: FeaturesService, private route: ActivatedRoute) { }

  ngOnInit() {

    this.routeSub = this.route.params.subscribe(params => {
      var projectId = params['projectId']; //log the value of id
      this.featureService.getFeatures(projectId).subscribe(features => {
        this.featureList = features;
      });
    });
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe();
  }

}
