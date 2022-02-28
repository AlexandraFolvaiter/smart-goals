import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { GoalWithDetails } from 'src/app/core/models/goals-with-details.model';
import { Objective } from 'src/app/core/models/objective.model';
import { GoalsService } from 'src/app/core/services/goals.service';
import { ObjectivesService } from 'src/app/core/services/objectives.service';
import { DeleteConfirmationComponent } from '../delete-confirmation/delete-confirmation.component';
import { ObjectiveManagementComponent } from '../objective-management/objective-management.component';

@Component({
  selector: 'app-goal-details',
  templateUrl: './goal-details.component.html',
  styleUrls: ['./goal-details.component.scss']
})
export class GoalDetailsComponent implements OnInit {
  id: any;
  showLoader = true;
  goal = new GoalWithDetails();
  
  constructor(
    private readonly goalsService: GoalsService,
    private readonly objectivesService: ObjectivesService,
    private readonly route: ActivatedRoute,
    private readonly snackBar: MatSnackBar,
    private readonly dialog: MatDialog) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.id = params.get('id');
    });

    this.getGoal();
  }

  getGoal() {
    this.showLoader = true;
    this.goalsService.getGoalById(this.id).subscribe((result) => {
      this.goal = result;
      this.showLoader = false;
    });
  }

  addObjective() {
    const dialogRef = this.dialog.open(ObjectiveManagementComponent, {
      width: '600px',
      height: '730px',
      data: { goalId: this.goal.id}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== null && result !== undefined) {
        this.objectivesService.add(result).subscribe(() => {
          this.getGoal();
          this.openSnackBar("The objective was added successfully!", "Success");
        }, (error) => {
          this.openSnackBar(`The objective couldn't be added! ${error.message}`, "Error");
        });
      }
    });
  }

  editObjective(objective: any) {
    const dialogRef = this.dialog.open(ObjectiveManagementComponent, {
      width: '600px',
      height: '730px',
      data: { goalId: this.goal.id, isEditMode: true, objective: objective }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== null && result !== undefined) {
        this.objectivesService.update(result).subscribe(() => {
          this.getGoal();
          this.openSnackBar("The objective was updated successfully!", "Success");
        }, (error) => {
          this.openSnackBar(`The objective couldn't be updated! ${error.message}`, "Error");
        });
      }
    });
  }

  deleteObjective(id: any) {
    const dialogRef = this.dialog.open(DeleteConfirmationComponent, {
      width: '400px',
      height: '170px'
    });
    
    dialogRef.afterClosed().subscribe(result => {
      if (result == true) {
        this.objectivesService.delete(id).subscribe(() => {
          this.getGoal();
          this.openSnackBar("The objective was deleted successfully!", "Success");
        }, (error) => {
          this.openSnackBar(`The objective couldn't be deleted! ${error.message}`, "Error");
        });
      }
    });    
  }

  updateIsFinished(obj: Objective) {
    const isFinished = obj.isFinished;
    this.objectivesService.updateIsFinished(obj.id, isFinished ? 'false' : 'true').subscribe(() => {
      this.openSnackBar(`The objective was marked as ${isFinished ? 'not finished' : 'finished'}!`, "Success");
    }, (error) => {
      this.openSnackBar(`The objective couldn't be updated! ${error.message}`, "Error");
    });
  }

  getObjectiveClass(isFinished: any) {
    return isFinished ? 'finished' : 'not-finished';
  }

  getProgress() {
    var finished = this.goal.objectives.filter(x => x.isFinished === true);

    if (this.goal.objectives != null && this.goal.objectives != undefined && this.goal.objectives.length == 0) {
      return '0%';
    }

    return ((100*finished.length)/this.goal.objectives.length) + '%';
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 3000
    });
  }
}
