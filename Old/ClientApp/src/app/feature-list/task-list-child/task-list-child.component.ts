import { Component, OnInit, Input } from '@angular/core';
import { Feature, FeatureTask } from '../../models/project';

@Component({
  selector: 'app-task-list-child',
  templateUrl: './task-list-child.component.html',
  styleUrls: ['./task-list-child.component.css']
})
export class TaskListChildComponent implements OnInit {

  @Input() feature: Feature;

  public tasks: FeatureTask[];

  constructor() { }

  ngOnInit() {
    this.tasks = this.feature.tasks;
  }

}
