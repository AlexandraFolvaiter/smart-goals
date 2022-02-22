import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { GoalsService } from './core/services/goals.service';
import { BaseService } from './core/services/base.service';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatCommonModule } from '@angular/material/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import { GoalsOverviewComponent } from './goals/goals-overview/goals-overview.component';
import { GoalsManagementComponent } from './goals/goals-management/goals-management.component';
import { GoalDetailsComponent } from './goals/goal-details/goal-details.component';
import { ObjectiveManagementComponent } from './goals/objective-management/objective-management.component';
import { ObjectivesService } from './core/services/objectives.service';
import { DeleteConfirmationComponent } from './goals/delete-confirmation/delete-confirmation.component';
import { ApplicationDetailsComponent } from './goals/application-details/application-details.component';

@NgModule({
  declarations: [
    AppComponent,
    GoalsOverviewComponent,
    GoalsManagementComponent,
    GoalDetailsComponent,
    ObjectiveManagementComponent,
    DeleteConfirmationComponent,
    ApplicationDetailsComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatCommonModule,
    MatToolbarModule,
    MatCardModule,
    MatGridListModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatInputModule,
    MatSelectModule,
    MatSnackBarModule,
    MatExpansionModule,
    MatCheckboxModule,
    MatProgressSpinnerModule
  ],
  providers: [BaseService, GoalsService, ObjectivesService],
  bootstrap: [AppComponent]
})
export class AppModule { }
