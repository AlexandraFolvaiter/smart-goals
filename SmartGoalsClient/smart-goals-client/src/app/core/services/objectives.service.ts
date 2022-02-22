import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GoalWithDetails } from '../models/goals-with-details.model';
import { Goal } from '../models/goals.model';
import { Objective } from '../models/objective.model';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class ObjectivesService {
  addPath = "Objectives/Add";
  updatePath = "Objectives/Update";
  updateIsFinishedPath = "Objectives/UpdateIsFinished";
  deletePath = "Objectives/Delete";

  constructor(protected base: BaseService) { }

  add(objective: Objective): Observable<any> {
    return this.base.post(this.addPath, objective);
  }

  update(objective: Objective): Observable<any> {
    return this.base.put(this.updatePath, objective);
  }

  updateIsFinished(id: string, isFinished: string): Observable<any> {
    return this.base.put(`${this.updateIsFinishedPath}?id=${id}&isFinished=${isFinished}`, null);
  }

  delete(id: string): Observable<any> {
    return this.base.delete(`${this.deletePath}?id=${id}`);
  }
}
