import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Goal } from 'src/app/core/models/goals.model';

@Component({
  selector: 'app-goals-management',
  templateUrl: './goals-management.component.html',
  styleUrls: ['./goals-management.component.scss']
})
export class GoalsManagementComponent implements OnInit {

  isEditMode = false;
  goal!: Goal;

  goalForm!: FormGroup;
  
  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {
    if (this.data !== null) {
      this.isEditMode = this.data.isEditMode ?? false;
      this.goal = this.data.goalValue ?? null;
    }
  }

  ngOnInit(): void {
    this. goalForm = new FormGroup({
      id: new FormControl(this.isEditMode ? this.goal?.id : null),
      title: new FormControl(this.isEditMode ? this.goal?.title : null, Validators.required),
      type: new FormControl(this.isEditMode ? this.goal?.type : null, Validators.required),
      status: new FormControl(this.isEditMode ? this.goal?.status :'NotStarted'),
    });
  }
}
