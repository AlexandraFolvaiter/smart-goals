import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Objective } from 'src/app/core/models/objective.model';

@Component({
  selector: 'app-objective-management',
  templateUrl: './objective-management.component.html',
  styleUrls: ['./objective-management.component.scss']
})
export class ObjectiveManagementComponent implements OnInit {

  isEditMode = false;
  goalId = '';
  objective!: Objective;
  objectiveForm!: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {
    if (this.data !== null) {
      this.goalId = this.data.goalId ?? false;
      this.isEditMode = this.data.isEditMode ?? false;
      this.objective = this.data.objective ?? null;
    }
   }

  ngOnInit(): void {
    this. objectiveForm = new FormGroup({
      id: new FormControl(this.isEditMode ? this.objective?.id : null),
      goalId: new FormControl(this.isEditMode ? this.objective?.goalId : this.goalId),
      name: new FormControl(this.isEditMode ? this.objective?.name : null, Validators.required),
      description: new FormControl(this.isEditMode ? this.objective?.description : null, Validators.required),
      timeEstimated: new FormControl(this.isEditMode ? this.objective?.timeEstimated : null, Validators.required),
      isFinished: new FormControl(this.isEditMode ? this.objective?.isFinished : false),
      specific: new FormControl(this.isEditMode ? this.objective?.specific : null),
      measurable: new FormControl(this.isEditMode ? this.objective?.measurable : null),
      achievable: new FormControl(this.isEditMode ? this.objective?.achievable : null),
      realistic: new FormControl(this.isEditMode ? this.objective?.realistic : null),
      timely: new FormControl(this.isEditMode ? this.objective?.timely : null),
    });
  }
}
