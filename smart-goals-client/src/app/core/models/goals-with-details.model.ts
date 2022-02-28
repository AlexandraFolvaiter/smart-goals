import { Goal } from "./goals.model";
import { Objective } from "./objective.model";

export class GoalWithDetails extends Goal {
  objectives = new Array<Objective>();
}