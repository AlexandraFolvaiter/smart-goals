import { AfterViewInit, Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Goal } from 'src/app/core/models/goals.model';
import { GoalsService } from 'src/app/core/services/goals.service';
import { DeleteConfirmationComponent } from '../delete-confirmation/delete-confirmation.component';
import { GoalsManagementComponent } from '../goals-management/goals-management.component';

@Component({
  selector: 'app-goals-overview',
  templateUrl: './goals-overview.component.html',
  styleUrls: ['./goals-overview.component.scss']
})
export class GoalsOverviewComponent implements OnInit, AfterViewInit{
  @ViewChild('gridParent') gridParent: any;

  showLoader = true;
  goals: Goal[] | undefined;
  columns = 4;
  constructor(
    private readonly goalsService: GoalsService,
    private readonly dialog: MatDialog,
    private snackBar: MatSnackBar) { }
  
  ngOnInit(): void {
    this.getAllGoals();
  }

  ngAfterViewInit() {
    this.setColNum();
  }

  @HostListener('window:resize', ['$event'])
  onResize() {
    this.setColNum();
  }

  getAllGoals() {
    this.showLoader = true;
    this.goalsService.getAllGoals().subscribe(result => {
      this.goals = result;
      this.showLoader = false;
    });
  }
  openAddModal() {
    const dialogRef = this.dialog.open(GoalsManagementComponent, {
      width: '400px',
      height: '290px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== null && result !== undefined) {
        this.goalsService.add(result).subscribe(() => {
          this.goals = [];
          this.getAllGoals();
          this.openSnackBar("The goal was added successfully!", "Success");
        }, (error) => {
          this.openSnackBar(`The goal couldn't be added! ${error.message}`, "Error");
        });
      }
    });
  }

  editGoal(goal: Goal) {
    const dialogRef = this.dialog.open(GoalsManagementComponent, {
      width: '400px',
      height: '365px',
      data: { isEditMode: true, goalValue: goal }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== null && result !== undefined) {
        this.goalsService.update(result).subscribe(() => {
          this.goals = [];
          this.getAllGoals();
          this.openSnackBar("The goal was updated successfully!", "Success");
        }, (error) => {
          this.openSnackBar(`The goal couldn't be updated! ${error.message}`, "Error");
        });
      }
    });
  }

  deleteGoal(id: any) {
    const dialogRef = this.dialog.open(DeleteConfirmationComponent, {
      width: '400px',
      height: '170px'
    });
    
    dialogRef.afterClosed().subscribe(result => {
      if (result == true) {
        this.goalsService.delete(id).subscribe(() => {
          this.goals = [];
          this.getAllGoals();
          this.openSnackBar("The goal was deleted successfully!", "Success");
          }, (error) => {
            this.openSnackBar(`The goal couldn't be deleted! ${error.message}`, "Error");
        });
      }
    }); 
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 3000
    });
  }

  setColNum(){
    let width = this.gridParent.nativeElement.offsetWidth;
    this.columns = Math.trunc(width / 320);
  }

  getIcon(type: any): string {
    switch (type) {
      case 'Health & Energy':
        return 'directions_bike';
      case "Career & Business":
        return 'work';
      case "Money & Finance":
        return 'monetization_on';
      case "Relationships & Love & Family":
        return 'favorite';
      case "Lifestyle":
        return 'local_bar';
      case "Personal Growth":
        return 'local_floristc';
      case "Social & Friends":
        return 'supervised_user_circle';
      case "Spirituality": 
        return 'waves';
      default:
        return 'home';
    }
  }

  getStatus(status: any) {
    switch (status) {
      case 'Not Started':
        return 'not-started';
      case "In Progress":
        return 'in-progress';
      case "Done":
        return 'done';
      default:
        return 'none';
    }
  }
}
