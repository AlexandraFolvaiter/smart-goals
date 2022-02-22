import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GoalDetailsComponent } from './goals/goal-details/goal-details.component';
import { GoalsOverviewComponent } from './goals/goals-overview/goals-overview.component';

const routes: Routes = [
   { path: '', redirectTo: 'goals', pathMatch: 'full' },
   { path: 'goals', component: GoalsOverviewComponent },
   { path: 'details/:id', component: GoalDetailsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
