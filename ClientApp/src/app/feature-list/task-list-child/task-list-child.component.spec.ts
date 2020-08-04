import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskListChildComponent } from './task-list-child.component';

describe('TaskListChildComponent', () => {
  let component: TaskListChildComponent;
  let fixture: ComponentFixture<TaskListChildComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TaskListChildComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskListChildComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
