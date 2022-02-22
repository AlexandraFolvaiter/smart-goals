import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GoalWithDetails } from '../models/goals-with-details.model';
import { Goal } from '../models/goals.model';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class GoalsService {
  getAllPath = "Goals/GetAll";
  getGoalByIdPath = "Goals/GetById";
  addPath = "Goals/Add";
  updatePath = "Goals/Update";
  deletePath = "Goals/Delete";

  constructor(protected base: BaseService) { }

  getAllGoals(): Observable<Goal[]> {
    return this.base.get(this.getAllPath);
  }

  getGoalById(id: string): Observable<GoalWithDetails> {
    return this.base.get(`${this.getGoalByIdPath}?id=${id}`);
  }

  add(goal: Goal): Observable<any> {
    return this.base.post(this.addPath, goal);
  }

  update(goal: Goal): Observable<any> {
    return this.base.put(this.updatePath, goal);
  }

  delete(id: string): Observable<any> {
    return this.base.delete(`${this.deletePath}?id=${id}`);
  }
}
